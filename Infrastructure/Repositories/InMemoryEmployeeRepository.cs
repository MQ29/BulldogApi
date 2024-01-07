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
    }
}
