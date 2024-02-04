using Bulldog.Infrastructure.Services.DTO;
using Shared.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Services
{
    public class ApiUserService : IApiUserService
    {
        private readonly HttpClient _httpClient;

        public ApiUserService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("ServerApi");
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
