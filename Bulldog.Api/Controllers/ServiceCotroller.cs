using Bulldog.Core.Repositories;
using Bulldog.Infrastructure.Commands.Users;
using Bulldog.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Bulldog.Api.Controllers
{
    [ApiController]
    [Route("api/services")]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public IActionResult GetAllServices()
        {
            var services = _serviceService.GetAll();
            return Ok(services);
        }

        [HttpGet("/services")]
        public IActionResult GetServices(Guid employeeId)
        {
            var service = _serviceService.GetByEmployeeIdAsync(employeeId);
            return Ok(service);
        }

        [HttpPost]
        public void Post([FromBody] CreateService request) //dodac employeeId
        {
            _serviceService.Create(request.Id,request.Name, request.Price, request.Duration, request.EmployeeId);
        }
    }
}
