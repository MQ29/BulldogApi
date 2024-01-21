using Bulldog.Core.Domain;
using Bulldog.Core.Repositories;
using Bulldog.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Repositories
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

        public async Task<IList<AvailableDate>> GetAsync(Guid employeeId)
        {
            return await _dbContext.AvailableDates.Where(x => x.EmployeeId == employeeId).ToListAsync();
        }

        public async Task<AvailableDate> GetByDayOfWeek(DayOfWeek dayOfWeek)
        {
            return await _dbContext.AvailableDates.FirstOrDefaultAsync(x => x.DayOfWeek == dayOfWeek);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync() >= 0);
        }

        public async Task Update(AvailableDate availableDate)
        {
            _dbContext.AvailableDates.Update(availableDate);
            await _dbContext.SaveChangesAsync();
        }
    }
}
