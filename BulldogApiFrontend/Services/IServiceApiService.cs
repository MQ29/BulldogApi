using Bulldog.Core.Domain;
using Bulldog.Infrastructure.Commands.AvailableDates;
using Bulldog.Infrastructure.Migrations;
using Bulldog.Infrastructure.Services.DTO;

namespace BulldogApiFrontend.Services
{
    public interface IServiceApiService
    {
        Task<EmployeeDto> GetByUserIdAsync(string userId);
        Task<EmployeeDto> GetByEmailAsync(string userMail);
        Task<IList<ServiceDto>> GetAllServies();
        Task<ServiceDto> Get(Guid Id);
        Task<IList<EmployeeDto>> GetEmployyesForServiceId(Guid Id);
        Task<IList<EmployeeDto>> GetAllEmployees();
        Task<IList<AvailableDateDto>> GetAvailableDates(Guid employeeId);
        Task<IList<AvailableHour>> GetAvailableHoursForDay(Guid employeeId, DateTime date);
        Task<IList<AvailableHour>> GetAllAvailableHours(Guid employeeId);
        Task AddAvailabilityDates(Guid employeeId, IList<AvailableDateDto> availableDates);
        Task<bool> UpdateAvailabilityDates(Guid EmployeeId,IList<AvailableDateDto> availableDates);
        Task AddReservation(Reservation reservation);
        Task Register(RegisterModel registerModel);
        Task<bool> UpdateAvailabileHours(Guid employeeId, UpdateAvailableHours updateAvailableHours);
        Task<ServiceDto> GetServiceById(Guid serviceId);
        Task<IList<ReservationDto>> GetReservations(Guid employeeId);
        Task<UserDto> GetUserByEmail(string email);
        Task<CompanyDto> GetCompanyByUserId(string userId);
        Task<bool> UpdateCompany(Guid companyId, UpdateCompanyDto updateAvailableHours);
        Task<IList<EmployeeDto>> GetAllEmployeesForCompanyId(Guid companyId);
        Task<IList<ServiceDto>> GetServicesByEmployeeId(Guid employeeId);
    }
}