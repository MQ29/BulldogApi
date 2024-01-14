using Bulldog.Core.Domain;
using Bulldog.Core.Repositories;
using Bulldog.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Services
{
    public class EFAvailableDateRepository : IAvailableDateRepository
    {
        private readonly BulldogDbContext _dbContext;

        public EFAvailableDateRepository(BulldogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(AvailableDate availableDate)
        {
            await _dbContext.AvailableDates.AddAsync(availableDate);
            await _dbContext.SaveChangesAsync();
        }
    }
}
