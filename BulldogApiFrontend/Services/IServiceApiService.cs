﻿using Bulldog.Core.Domain;
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
        Task<IList<AvailableHour>> GetAvailableHoursForDay(Guid employeeId, DateTime date);
        Task<IList<AvailableHour>> GetAllAvailableHours(Guid employeeId);
        Task AddAvailabilityDates(Guid employeeId, IList<AvailableDateDto> availableDates);
        Task<bool> UpdateAvailabilityDates(Guid EmployeeId,IList<AvailableDateDto> availableDates);
    }
}