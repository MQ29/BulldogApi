using Bulldog.Core.Domain;
using Bulldog.Infrastructure.Services;
using Bulldog.Infrastructure.Services.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bulldog.Api.Controllers
{
    [Route("Company")]
    [ApiController]
    public class CompanyControler : ControllerBase
    {
        private readonly ICompanyService _companyService;
        public CompanyControler(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get(Guid Id)
        {
            try
            {
                var company = await _companyService.GetCompanyAsync(Id);
                return Ok(company);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetForUserId(string userId)
        {
            try
            {
                var company = await _companyService.GetByUserIdAsync(userId);
                return Ok(company);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{companyId}")]
        public async Task<IActionResult> UpdateCompany(Guid companyId,[FromBody]UpdateCompanyDto request)
        {
            try
            {
                await _companyService.Update(companyId, request.Address, request.Name, request.PhoneNumber);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
