using Bulldog.Core.Domain;
using Bulldog.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Repositories
{
    public class InMemoryEmployeeRepository : IEmployeeRepository
    {
        private static ISet<Employee> _employees = new HashSet<Employee>();
        public async Task AddAsync(Employee employee)
        {
            _employees.Add(employee);
            await Task.CompletedTask;
        }

        public Task<IList<Employee>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Employee>> GetEmployeesForServiceIdAsync(Guid serviceId)
        {
            throw new NotImplementedException();
        }

        public void Reload(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
