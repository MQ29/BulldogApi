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
        public async Task<UserDto> Get(string email)
        => await _userService.GetAsync(email);

        //[HttpPost("")]
        //public async Task Post([FromBody]CreateUser request)
        //{
        //    await _userService.RegisterAsync(request.Email,request.Username,request.Password);
        //}

    }
}