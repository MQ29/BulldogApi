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
        Task<IList<Employee>> GetAllAsync();
        Task UpdateAsync(Employee employee);
        Task<IList<Employee>> GetEmployeesForServiceIdAsync(Guid serviceId);
        Task<bool> SaveChangesAsync();
        void Reload(Employee employee);
        //Task<EmployeeDto> GetEmployeeForUserId(Guid userId)
    }
}
