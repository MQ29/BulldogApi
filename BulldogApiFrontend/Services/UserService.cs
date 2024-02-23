using Bulldog.Infrastructure.Services.DTO;
using BulldogApiFrontend.Services.IServices;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using System.Text;
using Bulldog.Infrastructure.Migrations;
using System.Net.Http.Json;

namespace BulldogApiFrontend.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        public UserService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ServerApi");
        }
        public async Task<bool> UpdateUserIsConfigured(string userId, bool isConfigured)
        {
            try
            {
                var response = await _httpClient.PutAsync($"Users/{userId}/{isConfigured}", null);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw ex;
            }
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Users/{email}");
                if (!response.IsSuccessStatusCode)
                {
                    return null;
                }
                var user = await response.Content.ReadFromJsonAsync<UserDto>();
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
