using Bulldog.Core.Domain;
using Bulldog.Infrastructure.Commands.AvailableDates;
using Bulldog.Infrastructure.Services.DTO;

namespace BulldogApiFrontend.Services
{
    public interface IServiceApiService
    {
        Task<IList<ServiceDto>> GetAllServies();
        Task<ServiceDto> Get(Guid Id);
        Task<IList<EmployeeDto>> GetEmployyesForServiceId(Guid Id);
        Task<IList<EmployeeDto>> GetAllEmployees();
        Task<IList<AvailableDateDto>> GetAvailableDates(Guid employeeId);
        Task<AvailableDateDto> SaveAvailability(Guid EmployeeId, CreateAvailableDate availableDates);
    }
}