using Bulldog.Infrastructure.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Services
{
    public interface IServiceService
    {
        Task<ServiceDto> GetAsync(Guid Id);
        Task Create(Guid employeeId, string name, decimal price, int duration);
        Task<IList<ServiceDto>> GetByEmployeeIdAsync(Guid employeeId);
        Task<IList<ServiceDto>> GetAll();
        Task RemoveAsync(Guid Id);
        Task<IList<ServiceDto>> GetByCompanyIdAsync(Guid companyId);


    }
}
