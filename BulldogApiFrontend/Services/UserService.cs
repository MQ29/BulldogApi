﻿using Bulldog.Infrastructure.Services.DTO;
using BulldogApiFrontend.Services.IServices;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;
using System.Text;
using Bulldog.Infrastructure.Migrations;

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

    }
}
