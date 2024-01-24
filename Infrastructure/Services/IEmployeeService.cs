using Bulldog.Core.Domain;
using Bulldog.Infrastructure.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bulldog.Infrastructure.Services
{
    public interface IEmployeeService
    {
        Task Create(string email);
        Task RemoveAsync(Guid Id);
        Task<EmployeeDto> GetById(Guid Id);
        Task<EmployeeDto> GetByEmail(string email);
        Task<IList<EmployeeDto>> GetByServiceId(Guid Id);
        Task<IList<EmployeeDto>> GetAll();
        Task AddAvailableDate(Guid employeeId, IList<AvailableDateDto> availableDates);
        Task<IList<AvailableDateDto>> GetAvailableDates(Guid Id);
        Task UpdateAvailableDates(Guid employeeId, IList<AvailableDateDto> availableDates);
        Task<IList<AvailableHour>> GetAvailableHours(Guid employeeId, DateTime date);
        Task<IList<AvailableHour>> GetAllAvailableHours(Guid Id);
    }
}
