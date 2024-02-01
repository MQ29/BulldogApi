using Bulldog.Core.Domain;
using Bulldog.Core.Repositories;
using Bulldog.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public async Task<IList<AvailableHour>> GetAvailableForDayAsync(Guid employeeId, DateTime? date)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);
            if (employee != null)
            {
                var availableHours = await _dbContext.AvailableHours.Where(x => x.EmployeeId == employeeId && x.IsAvailable && x.Hour.Date == date.Value.Date)
                    .OrderBy(x => x.Hour).ToListAsync();
                if (availableHours.Count > 0)
                {
                    return availableHours;
                }
            }
            throw new Exception($"employee with id {employeeId} wasnt found.");
        }

        public async Task<IList<AvailableHour>> GetAvailableHours(Guid Id)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == Id);
            if (employee != null)
            {
                return await _dbContext.AvailableHours.Where(x => x.EmployeeId == Id && x.IsAvailable).ToListAsync();
            }
            throw new Exception($"employee with id {Id} wasnt found.");
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync() >= 0);
        }

        public void Update(AvailableHour availableHour)
        {
            var existingAvailableHour = _dbContext.AvailableHours.Find(availableHour.Id);
            if (existingAvailableHour is null)
            {
                throw new Exception($"Error: No AvailableHour found with Id: {availableHour.Id}");
            }
            existingAvailableHour.IsAvailable = availableHour.IsAvailable;
            _dbContext.SaveChanges();
        }
    }
}
