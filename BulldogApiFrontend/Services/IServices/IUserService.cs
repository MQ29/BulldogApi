using Bulldog.Infrastructure.Services.DTO;

namespace BulldogApiFrontend.Services.IServices
{
    public interface IUserService
    {
        Task<bool> UpdateUserIsConfigured(string userId, bool isConfigured);
        Task<UserDto> GetUserByEmail(string email);

    }
}
