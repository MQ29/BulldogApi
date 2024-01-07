using Bulldog.Infrastructure.Commands.Employees;
using Bulldog.Infrastructure.Commands.Reservations;
using Bulldog.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bulldog.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateEmployee request)
        {
            await _employeeService.Create(request.UserId);
            return Ok();
        }
    }
}
