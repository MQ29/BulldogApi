using Azure;
using Bulldog.Core.Domain;
using Bulldog.Infrastructure.Services.DTO;
using BulldogApiFrontend.Services.IServices;
using System.Net.Http.Json;

namespace BulldogApiFrontend.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly HttpClient _httpClient;
        public CompanyService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ServerApi");
        }
        public Task Create(Company company)
        {
            throw new NotImplementedException();
        }

        public Task<CompanyDto> GetByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<CompanyDto> GetCompanyAsync(Guid companyId)
        {
            var response = await _httpClient.GetAsync($"Company/{companyId}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var company = await response.Content.ReadFromJsonAsync<CompanyDto>();
            return company;
        }

        public Task Update(Guid companyId, Address address, string name, string phoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}
