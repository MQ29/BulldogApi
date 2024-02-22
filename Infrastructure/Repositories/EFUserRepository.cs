using Bulldog.Core.Domain;
using Bulldog.Core.Repositories;
using Bulldog.Infrastructure.EF;
using Bulldog.Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Repositories
{
    public class EFUserRepository : IUserRepository
    {
        private readonly BulldogDbContext _dbContext;

        public EFUserRepository(BulldogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetByEmail(string email)
        {
            var user =  await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
            return user;
        }

        public async Task<User> GetByUserId(string Id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task RemoveAsync(string Id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == Id);
            if (user == null)
                throw new Exception($"User with id: {Id} wasnt found.");
            _dbContext.Remove(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            var existingUser = _dbContext.Users.Find(user.Id);
            if (existingUser is null)
            {
                throw new Exception($"Error: No user found with Id: {existingUser.Id}");
            }
            existingUser.IsConfigured = user.IsConfigured;
            await _dbContext.SaveChangesAsync();
        }

    }
}
