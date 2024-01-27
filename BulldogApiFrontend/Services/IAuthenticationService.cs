using Bulldog.Core.Domain;

namespace BulldogApiFrontend.Services
{
    public interface IAuthenticationService
    {
        event Action<string?>? LoginChange;
        ValueTask<string> GetJwtAsync();
        Task<DateTime> LoginAsync(LoginModel model);
        Task LogoutAsync();
        Task<bool> RefreshAsync();
    }
}
