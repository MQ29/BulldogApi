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
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
        }
    }
}
