using Bulldog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Core.Repositories
{
    public interface IEmployeeRepository
    {
        Task AddAsync(Employee employee);
        Task RemoveAsync(Guid Id);
        Task<Employee> GetAsync(Guid Id);
    }
}
