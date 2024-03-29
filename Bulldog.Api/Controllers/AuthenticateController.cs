﻿using Bulldog.Core.Domain;
using Bulldog.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Bulldog.Api.Controllers
{
    [Route("Authenticate")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly IEmployeeService _employeeService;
        private readonly ICompanyService _companyService;

        public AuthenticateController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager,
            IConfiguration configuration, IEmployeeService employeeService, ICompanyService companyService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _employeeService = employeeService;
            _companyService = companyService;
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];

            using var generator = RandomNumberGenerator.Create();

            generator.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }

        private JwtSecurityToken GenerateJwt(string username)
        {
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["JWT:Secret"] ?? throw new InvalidOperationException("Secret not configured")));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.UtcNow.AddSeconds(30),
                claims: authClaims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim("IsConfigured",user.IsConfigured.ToString(), ClaimValueTypes.Boolean),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);
                var refreshToken = GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiry = DateTime.Now.AddMinutes(1);

                await _userManager.UpdateAsync(user);

                return Ok(new LoginResponse
                {
                    JwtToken = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo,
                    RefreshToken = refreshToken
                });
            }
            return Unauthorized();
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshModel model)
        {
            await Console.Out.WriteLineAsync("Refresh called");

            var principal = GetPrincipalFromExpiredToken(model.AccessToken);

            if (principal?.Identity?.Name is null)
                return Unauthorized();

            var user = await _userManager.FindByNameAsync(principal.Identity.Name);

            if (user is null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiry < DateTime.UtcNow)
                return Unauthorized();

            var token = GenerateJwt(principal.Identity.Name);

            await Console.Out.WriteLineAsync("Refresh Sukces!");

            return Ok(new LoginResponse
            { 
                JwtToken = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo,
                RefreshToken = model.RefreshToken
            });
        }

        [Authorize]
        [HttpDelete("Revoke")]
        public async Task<IActionResult> Revoke()
        {
            await Console.Out.WriteLineAsync("Revoke called");

            var username = HttpContext.User.Identity?.Name;

            if (username is null) 
                return Unauthorized();

            var user = await _userManager.FindByNameAsync(username);

            if (user is null)
                return Unauthorized();

            user.RefreshToken = null;

            await _userManager.UpdateAsync(user);

            await Console.Out.WriteLineAsync("Revoke sukces.pl!");

            return Ok();
        }

        private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var secret = _configuration["JWT:Secret"] ?? throw new InvalidOperationException("Secret not configured");

            var validation = new TokenValidationParameters()
            {
                ValidAudience = _configuration["JWT:ValidAudience"],
                ValidIssuer = _configuration["JWT:ValidIssuer"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                ValidateLifetime = false
            };

            return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Email address already registered." });

            User user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                Fullname = model.Fullname,
                UserName = model.Fullname
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            User user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                Fullname = model.Fullname
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }


        [HttpPost]
        [Route("register-employee")]
        public async Task<IActionResult> RegisterEmployee([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Email address already registered." });

            User user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                Fullname = model.Fullname,
                UserName = model.Fullname.Trim()
            };
            
            //todo employee?
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }
            //await _employeeService.Create(model.Email);
            //TODO

            if (!await _roleManager.RoleExistsAsync(UserRoles.Employee))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Employee));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Employee))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Employee);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.Employee))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("register-owner")]
        public async Task<IActionResult> RegisterOwner([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Message = "Email address already registered." });

            User user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                Fullname = model.Fullname,
                UserName = model.Email,
                IsConfigured = false
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            }
            else
            {
                Company company = new Company(user);
                await _companyService.Create(company);
                Employee employee = new Employee(user, company.Id);
                await _employeeService.CreateForUser(employee);
                if (!await _roleManager.RoleExistsAsync(UserRoles.Employee))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Employee));

                if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                if (!await _roleManager.RoleExistsAsync(UserRoles.Owner))
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Owner));

                if (await _roleManager.RoleExistsAsync(UserRoles.Employee))
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.Employee);
                }

                if (await _roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.User);
                }

                if (await _roleManager.RoleExistsAsync(UserRoles.Owner))
                {
                    await _userManager.AddToRoleAsync(user, UserRoles.Owner);
                }

                return Ok(new Response { Status = "Success", Message = "User created successfully!" });
            }
        }
    }
}
