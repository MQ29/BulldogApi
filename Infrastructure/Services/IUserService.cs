using Bulldog.Core.Domain;
using Bulldog.Infrastructure.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Services
{
    public interface IUserService
    {
        User ValidateUserCredentials(string? username, string? password);
        Task<UserDto> GetAsync(string email);
    }
}
