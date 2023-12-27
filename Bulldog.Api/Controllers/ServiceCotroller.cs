using Bulldog.Core.Repositories;
using Bulldog.Infrastructure.Commands.Users;
using Bulldog.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bulldog.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet("{name}")]
        public IActionResult Get(string name)
        {
            var service = _serviceService.Get(name);
            return Ok(service);
        }

        [HttpPost]
        public void Post([FromBody] CreateService request)
        {
            _serviceService.Create(request.Name, request.Price, request.Duration);
        }
    }
}
