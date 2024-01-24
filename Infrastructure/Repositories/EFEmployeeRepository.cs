using Bulldog.Core.Domain;
using Bulldog.Core.Repositories;
using Bulldog.Infrastructure.EF;
using Bulldog.Infrastructure.Services.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Repositories
{
    public class EFEmployeeRepository : IEmployeeRepository
    {
        private readonly BulldogDbContext _dbContext;

        public EFEmployeeRepository(BulldogDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Employee employee)
        {
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<IList<Employee>> GetAllAsync()
        {
            return await _dbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetAsync(Guid Id)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task RemoveAsync(Guid Id)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == Id);
            if (employee == null)
            {
                throw new Exception($"Employee with Id: {Id} wasnt found.");
            }
            _dbContext.Employees.Remove(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IList<Employee>> GetEmployeesForServiceIdAsync(Guid serviceId)
        {
            var employees = await _dbContext.Employees.Where(x => x.Services.Any(x => x.Id == serviceId)).ToListAsync();
            return employees;
        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _dbContext.SaveChangesAsync() >= 0);
        }

        public void Reload(Employee employee)
        {
            _dbContext.Entry(employee).Reload();
        }

        public async Task<Employee> GetByEmailAsync(string email)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<Employee> GetByUserIdAsync(string userId)
        {
            return await _dbContext.Employees.FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
