using Bulldog.Core.Domain;
using Bulldog.Infrastructure.Commands.AvailableDates;
using Bulldog.Infrastructure.Commands.Employees;
using Bulldog.Infrastructure.Commands.Reservations;
using Bulldog.Infrastructure.Services;
using Bulldog.Infrastructure.Services.DTO;
using Microsoft.AspNetCore.Authorization;
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

        [HttpGet("{email}")]
        public async Task<IActionResult> GetByEmail(string email)
        {
            var employee = await _employeeService.GetByEmail(email);
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
            await _employeeService.Create(request.Email);
            return Ok();
        }

        [HttpDelete("employees/{Id}")]
        public async Task<IActionResult> Remove(Guid Id)
        {

            await _employeeService.RemoveAsync(Id);
            return NoContent();
        }

        [HttpPost("InsertAvailableDate/{EmployeeId}")]
        public async Task<IActionResult> InsertAvailableDate(Guid EmployeeId, [FromBody] IList<AvailableDateDto> availableDates)
        {
            try
            {
                await _employeeService.AddAvailableDate(EmployeeId, availableDates);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
        //ToDo Put InsertAvailableDate/{EmployeeId}
        [HttpPut("UpdateAvailableDates/{EmployeeId}")]
        public async Task<IActionResult> UpdateAvailableDate(Guid EmployeeId, [FromBody] IList<AvailableDateDto> availableDates)
        {
            try
            {
                await _employeeService.UpdateAvailableDates(EmployeeId, availableDates);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        [HttpGet("{Id}/availableDates")]
        public async Task<IActionResult> GetAvailableDates(Guid Id)
        {
            var availableDates = await _employeeService.GetAvailableDates(Id);
            return Ok(availableDates);
        }

        [HttpGet("{Id}/availableHours/{date}")]
        public async Task<IActionResult> GetAvailableHoursForDay(Guid Id, DateTime date)
        {
            var availableHours = await _employeeService.GetAvailableHours(Id, date);
            return Ok(availableHours);
        }

        [HttpGet("{Id}/availableHours")]
        public async Task<IActionResult> GetAvailableHours(Guid Id)
        {
            var availableHours = await _employeeService.GetAllAvailableHours(Id);
            return Ok(availableHours);
        }


    }
}
