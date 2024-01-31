using Bulldog.Infrastructure.Commands.Users;
using Bulldog.Infrastructure.Services;
using Bulldog.Infrastructure.Services.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Bulldog.Api.Controllers
{
    [Route("Users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<UserDto>> Get(string email)
        {
            try
            {
                var user = await _userService.GetAsync(email);

                if (user is null)
                {
                    return NotFound($"User with email: {email} was not found.");
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"User with email: {email} was not found");
                return null;
            }
        }

        //[HttpPost("")]
        //public async Task Post([FromBody]CreateUser request)
        //{
        //    await _userService.RegisterAsync(request.Email,request.Username,request.Password);
        //}

    }
}
