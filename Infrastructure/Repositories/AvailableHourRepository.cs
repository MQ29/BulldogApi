using Bulldog.Core.Domain;
using Bulldog.Core.Repositories;
using Bulldog.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Repositories
{
    public class AvailableHourRepository : IAvailableHourRepository
    {
        private readonly BulldogDbContext _dbContext;
        public AvailableHourRepository(BulldogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(AvailableHour hour)
        {
            await _dbContext.AvailableHours.AddAsync(hour);
            await _dbContext.SaveChangesAsync();
        }

        public async Task EmptyTableAsync()
        {
            var entitiesToRemove = await _dbContext.AvailableHours.Where(x => x.IsAvailable == true).ToListAsync();

            if (entitiesToRemove.Any())
            {
                _dbContext.AvailableHours.RemoveRange(entitiesToRemove);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync() >= 0);
        }
    }
}
