using Bulldog.Core.Repositories;
using Bulldog.Infrastructure.Commands.Users;
using Bulldog.Infrastructure.Services;
using Bulldog.Infrastructure.Services.DTO;
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
        public async Task<IList<ServiceDto>> GetAllServices()
        {
            return await _serviceService.GetAll();
        }

        [HttpGet("/services/{employeeId}")]
        public async Task<IList<ServiceDto>> GetServices(Guid employeeId)
        {
            return await _serviceService.GetByEmployeeIdAsync(employeeId);
        }

        [HttpPost]
        public async Task Post([FromBody] CreateService request) //dodac employeeId
        {
            await _serviceService.Create(request.Id,request.Name, request.Price, request.Duration, request.EmployeeId);
        }
    }
}
