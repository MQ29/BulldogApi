﻿using Bulldog.Core.Domain;
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
        //Task Create(string email);
        Task CreateForUser(Employee employee);
        Task RemoveAsync(Guid Id);
        Task<EmployeeDto> GetById(Guid Id);
        Task<EmployeeDto> GetByUserId(string userId);
        Task<EmployeeDto> GetByEmail(string email);
        Task<IList<EmployeeDto>> GetByServiceId(Guid Id);
        Task<IList<EmployeeDto>> GetAll();
        Task AddAvailableDate(Guid employeeId, IList<AvailableDateDto> availableDates);
        Task<IList<AvailableDateDto>> GetAvailableDates(Guid Id);
        Task UpdateAvailableDates(Guid employeeId, IList<AvailableDateDto> availableDates);
        Task<IList<AvailableHour>> GetAvailableHours(Guid employeeId, DateTime? date);
        Task<IList<AvailableHour>> GetAllAvailableHours(Guid Id);
        Task UpdateAvailableHours(Guid EmployeeId, int duration, DateTime? selectedHour);
        Task<IList<EmployeeDto>> GetByCompanyId(Guid companyId);
    }
}
