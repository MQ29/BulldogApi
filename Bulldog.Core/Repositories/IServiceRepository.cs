using Bulldog.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Core.Repositories
{
    public interface IServiceRepository  
    {
        Task<Service> GetByIdAsync(Guid Id);
        Task<IList<Service>> GetAllAsync();
        Task<IList<Service>> GetByEmployeeIdAsync(Guid employeeId);
        Task AddAsync(Service service);
        Task UpdateAsync(Service service);
        Task RemoveAsync(Guid Id);
        Task<IList<Service>> GetByCompanyId(Guid companyId);
    }
}
