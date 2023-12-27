﻿using Bulldog.Core.Domain;
using Bulldog.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class InMemoryUserRepository : IUserRepository
    {
        private static ISet<User> _users = new HashSet<User>()
        {
            new User("user1@gmail.com","user1","secret","salt"),
            new User("user2@gmail.com","user2","secret","salt"),
            new User("user3@gmail.com","user3","secret","salt")

        };

       
        public async Task<User> GetAsync(Guid Id)
        => await Task.FromResult(_users.SingleOrDefault(x => x.Id == Id));

        public async Task<User> GetAsync(string email)
        => await Task.FromResult(_users.SingleOrDefault(x => x.Email == email));


        public async Task<IEnumerable<User>> GetAllAsync()
        => await Task.FromResult(_users);
        public async Task AddAsync(User user)
        {
             _users.Add(user);
            await Task.CompletedTask;
        }
        public async Task RemoveAsync(Guid Id)
        {
            var user = await GetAsync(Id);
            _users.Remove(user);
            await Task.CompletedTask;

        }

        public async Task UpdateAsync(User user)
        {
            await Task.CompletedTask;
        }
    }
}
