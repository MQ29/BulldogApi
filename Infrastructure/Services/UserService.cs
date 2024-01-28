using AutoMapper;
using Bulldog.Core.Domain;
using Bulldog.Core.Repositories;
using Bulldog.Infrastructure.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> GetAsync(string email)
        {
            var user = await _userRepository.GetByEmail(email);
            if (user is null)
            {
                throw new Exception($"User with email: {email} wansnt found.");
            }
            var mappedUser = _mapper.Map<UserDto>(user);
            return mappedUser;
        }

        public User ValidateUserCredentials(string? username, string? password)
        {
            throw new Exception();
        }
    }
}
