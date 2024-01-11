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
        Task Create(Guid Id, string name, decimal price, int duration, Guid employeeId);
        Task<ServiceDto> GetByEmployeeIdAsync(Guid employeeId);
        Task<IList<ServiceDto>> GetAll();


    }
}
