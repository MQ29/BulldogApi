using Bulldog.Core.Domain;
using Bulldog.Infrastructure.Commands.AvailableDates;
using Bulldog.Infrastructure.Commands.Employees;
using Bulldog.Infrastructure.Commands.Reservations;
using Bulldog.Infrastructure.Services;
using Bulldog.Infrastructure.Services.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bulldog.Api.Controllers
{
    [Route("employees")]
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

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _employeeService.GetAll();
            return Ok(employees);
        }

        [HttpGet("by-service/{Id}")]
        public async Task<IActionResult> GetByServiceId(Guid Id)
        {
            var employees = await _employeeService.GetByServiceId(Id);
            return Ok(employees);
        }

        //TODO Employees for service id 

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

        [HttpPost("InsertAvailableDate")]
        public async Task<IActionResult> InsertAvailableDate(Guid employeeId, [FromBody] CreateAvailableDate availableDate)
        {
            await _employeeService.AddAvailableDate(employeeId, availableDate.DayOfWeek, availableDate.IsOpen, 
                availableDate.WorkingHours);
            return Ok();
            // Logic to associate the available date with the specified employee
            // and save it to the data,base

        }

        [HttpGet("{Id}/availableDates")]
        public async Task<IActionResult> GetAvailableDates(Guid Id)
        {
            var availableDates = await _employeeService.GetAvailableDates(Id);
            return Ok(availableDates);
        }

        

    }
}
