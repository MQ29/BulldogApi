using Bulldog.Infrastructure.Commands.Employees;
using Bulldog.Infrastructure.Commands.Reservations;
using Bulldog.Infrastructure.Services;
using Bulldog.Infrastructure.Services.DTO;
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

        [HttpGet("employees/{Id}")]
        public async Task<IActionResult> Get(Guid Id)
        {
            var employee = await _employeeService.GetById(Id);
            return Ok(employee);
        }

        [HttpGet("employees")]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeService.GetAll();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateEmployee request)
        {
            await _employeeService.Create(request.UserId);
            return Ok();
        }

        [HttpDelete("employees/{Id}")]
        public async Task<IActionResult> Remove(Guid Id)
        {

            await _employeeService.RemoveAsync(Id);
            return NoContent();
        }
    }
}
