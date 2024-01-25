using Bulldog.Core.Repositories;
using Bulldog.Infrastructure.Commands.Users;
using Bulldog.Infrastructure.Services;
using Bulldog.Infrastructure.Services.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace Bulldog.Api.Controllers
{
    [ApiController]
    [Route("api/services")] //route do zmiany
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet("/services/{Id}")]
        public async Task<IActionResult> GetService(Guid Id)
        {
           var service = await _serviceService.GetAsync(Id);
           return Ok(service);
        }

        [HttpGet]
        public async Task<IList<ServiceDto>> GetAllServices()
        {
            return await _serviceService.GetAll();
        }

        [HttpGet("/services/by-employee/{employeeId}")]
        public async Task<IList<ServiceDto>> GetServices(Guid employeeId)
        {
            return await _serviceService.GetByEmployeeIdAsync(employeeId);
        }

        [HttpPost]
        public async Task Post(Guid employeeId,[FromBody] CreateService request) //dodac employeeId
        {
            await _serviceService.Create(employeeId, request.Name, request.Price, request.Duration);
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(Guid Id)
        {
            await _serviceService.RemoveAsync(Id);
            return NoContent();
        }
    }
}
