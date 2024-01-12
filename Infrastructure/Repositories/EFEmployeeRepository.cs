using Bulldog.Core.Domain;
using Bulldog.Core.Repositories;
using Bulldog.Infrastructure.EF;
using Bulldog.Infrastructure.Services.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var employeeToFind = _dbContext.Employees.FirstOrDefault(x => x.UserId == employee.UserId);
            if (employeeToFind != null)
            {
                throw new Exception($"Employee with userID: {employee.UserId} already exists.");
            }
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Employee> GetAsync(Guid Id)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == Id);
            if (employee == null)
            {
                throw new Exception($"Employee with Id: {Id} wasnt found.");
            }
            return employee;
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
    }
}
