using Bulldog.Core.Domain;
using Bulldog.Infrastructure.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Services
{
    public interface IEmployeeService
    {
        Task Create(Guid userId);
        Task RemoveAsync(Guid Id);
        Task<EmployeeDto> GetById(Guid Id);
        Task<IList<EmployeeDto>> GetAll();
        Task AddAvailableDate(Guid Id, DateTime startTime, DateTime endTime,
            string title, string description, string color); 
    }
}
