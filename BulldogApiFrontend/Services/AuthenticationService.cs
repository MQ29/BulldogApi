using Blazored.LocalStorage;
using Bulldog.Core.Domain;
using BulldogApiFrontend.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Threading.Channels;

namespace BulldogApiFrontend.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IHttpClientFactory _factory;
        private ILocalStorageService _localStorageService;
        private string JWT_KEY = nameof(JWT_KEY);
        private string REFRESH_KEY = nameof(REFRESH_KEY);
        private string? _jwtCache;
        private readonly AuthenticationStateProvider _authenticationState;
        public AuthenticationService(IHttpClientFactory factory, ILocalStorageService localStorageService, AuthenticationStateProvider authenticationStateProvider)
        {
            _factory = factory;
            _localStorageService = localStorageService;
            _authenticationState = authenticationStateProvider;
        }

        public event Action<string?>? LoginChange;

        public async ValueTask<string> GetJwtAsync()
        {
            if (string.IsNullOrEmpty(_jwtCache))
                _jwtCache = await _localStorageService.GetItemAsync<string>(JWT_KEY);

            return _jwtCache;
        }

        public async Task<DateTime> LoginAsync(LoginModel model)
        {
            var response = await _factory.CreateClient("ServerApi").PostAsJsonAsync("Authenticate/login", model);

            if (!response.IsSuccessStatusCode)
            {
                throw new UnauthorizedAccessException("Login failed.");
            }

            var content = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (content is null)
            {
                throw new InvalidDataException();
            }

            await _localStorageService.SetItemAsync(JWT_KEY, content.JwtToken);
            await _localStorageService.SetItemAsync(REFRESH_KEY, content.RefreshToken);

            (_authenticationState as CustomAuthProvider).NotifyAuthState();
            LoginChange?.Invoke(GetUsername(content.JwtToken));

            return content.Expiration;
        }

        public async Task LogoutAsync()
        {
            var response = await _factory.CreateClient("ServerApi").DeleteAsync("Authenticate/revoke");

            await _localStorageService.RemoveItemAsync(JWT_KEY);
            await _localStorageService.RemoveItemAsync(REFRESH_KEY);

            _jwtCache = null;

            await Console.Out.WriteLineAsync($"Revoke gave response {response.StatusCode}");

            LoginChange?.Invoke(null);
        }

        private static string GetUsername(string token)
        {
            var jwt = new JwtSecurityToken(token);
            return jwt.Claims.First(x => x.Type == ClaimTypes.Name).Value;
        }

        public async Task<bool> RefreshAsync()
        {
            var model = new RefreshModel
            { 
                AccessToken = await _localStorageService.GetItemAsync<string>(JWT_KEY),
                RefreshToken = await _localStorageService.GetItemAsync<string>(REFRESH_KEY)
            };

            var response = await _factory.CreateClient("ServerApi").PostAsJsonAsync("Authenticate/refresh", model);

            if (!response.IsSuccessStatusCode)
            {
                await LogoutAsync();
                return false;
            }

            var content = await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (content is null)
                throw new InvalidDataException();

            await _localStorageService.SetItemAsync(JWT_KEY, content.JwtToken);
            await _localStorageService.SetItemAsync(REFRESH_KEY, content.RefreshToken);

            _jwtCache = content.JwtToken;

            return true;
        }
    }
}
