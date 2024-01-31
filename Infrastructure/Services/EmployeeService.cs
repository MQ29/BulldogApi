using AutoMapper;
using Bulldog.Core.Domain;
using Bulldog.Core.Repositories;
using Bulldog.Infrastructure.Commands.AvailableDates;
using Bulldog.Infrastructure.Services.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Bulldog.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAvailableDateRepository _availableDateRepository;
        private readonly IAvailableHourRepository _availableHourRepository;
        private readonly IUserRepository _userRepository;

        public EmployeeService(IMapper mapper, IEmployeeRepository employeeRepository,
             IAvailableDateRepository availableDateRepository, IAvailableHourRepository availableHourRepository, IUserRepository userRepository)

        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _availableDateRepository = availableDateRepository;
            _availableHourRepository = availableHourRepository;
            _userRepository = userRepository;
        }

        public async Task AddAvailableDate(Guid employeeId, IList<AvailableDateDto> availableDates)
        {
            try
            {
                var employee = await _employeeRepository.GetAsync(employeeId);
                if (employee == null)
                {
                    throw new InvalidOperationException($"Employee with id {employeeId} not found.");
                }
                var mapppedAvilableDates = _mapper.Map<List<AvailableDate>>(availableDates);
                foreach (var availableDate in mapppedAvilableDates)
                {
                    employee.AddAvailableDate(availableDate);
                    await _availableDateRepository.AddAsync(availableDate);
                }
                DateTime currentDate = DateTime.Today;

                for (int i = 0; i < 14; i++) //2 tygodnie, magic number powinno sie to dac ustawic
                {
                    var availableDateByDay = await _availableDateRepository.GetByDayOfWeek(currentDate.DayOfWeek);
                    if (availableDateByDay.IsOpen)
                    {
                        Console.WriteLine($"Otwarte:{availableDateByDay.DayOfWeek}");
                        DateTime Interval = currentDate + availableDateByDay.WorkingHours.StartTime;

                        while (Interval < currentDate + availableDateByDay.WorkingHours.EndTime)
                        {
                            var availableHour = new AvailableHour(employee.Id, Interval);
                            employee.AddAvailableHour(availableHour);
                            await _availableHourRepository.AddAsync(availableHour);
                            Interval = Interval.AddMinutes(15);
                        }
                        currentDate = currentDate.AddDays(1);
                    }
                    else
                    {
                        currentDate = currentDate.AddDays(1);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        public async Task Create(string email)
        {
            var user = await _userRepository.GetByEmail(email);
            var employee = await _employeeRepository.GetByEmailAsync(email);
            if (employee != null)
            {
                throw new Exception($"Employee with id: '{email}' already exists.");
            }
            employee = new Employee(user);
            await _employeeRepository.AddAsync(employee);
        }

        public async Task<IList<EmployeeDto>> GetAll()
        {
            var employees = await _employeeRepository.GetAllAsync();
            var employeesToReturn = new List<EmployeeDto>();
            foreach (var employee in employees)
            {
                var employeeToReturn = _mapper.Map<EmployeeDto>(employee);
                employeesToReturn.Add(employeeToReturn);
            }
            return employeesToReturn;
        }

        public async Task<IList<AvailableHour>> GetAllAvailableHours(Guid Id)
        {
            return await _availableHourRepository.GetAvailableHours(Id);
        }

        public async Task<IList<AvailableDateDto>> GetAvailableDates(Guid Id)
        {
            var availableDates = await _availableDateRepository.GetAsync(Id);
            var availableDatesToReturn = _mapper.Map<IList<AvailableDateDto>>(availableDates);
            return availableDatesToReturn;
        }//TODO: Validation

        public async Task<IList<AvailableHour>> GetAvailableHours(Guid employeeId, DateTime? date)
        {
            var employee = await _employeeRepository.GetAsync(employeeId);
            if (employee is null)
            {
                throw new Exception($"Employee with id: {employeeId} wasnt found.");
            }
            return await _availableHourRepository.GetAvailableForDayAsync(employeeId, date);
        }

        public async Task<EmployeeDto> GetById(Guid Id)
        {
            var employee = await _employeeRepository.GetAsync(Id);
            if (employee is null)
            {
                throw new Exception($"Error while getting by Id: {Id}");
            }
            var employeeToReturn = _mapper.Map<EmployeeDto>(employee);
            return employeeToReturn;

        }

        public async Task<IList<EmployeeDto>> GetByServiceId(Guid Id)
        {
            var employees = await _employeeRepository.GetEmployeesForServiceIdAsync(Id);
            if (employees.Count == 0)
            {
                throw new Exception($"No employees found for service with id: {Id}");
            }
            var employeesDto = _mapper.Map<IList<EmployeeDto>>(employees);
            return employeesDto;

        }

        public async Task<EmployeeDto> GetByEmail(string email)
        {
            var employee = await _employeeRepository.GetByEmailAsync(email);
            if (employee is null)
            {
                throw new Exception($"Employee with email: {email} wasnt found.");
            }
            var mappedemployee = _mapper.Map<EmployeeDto>(employee);
            return mappedemployee;

        }

        public async Task RemoveAsync(Guid Id)
        {
            await _employeeRepository.RemoveAsync(Id);
        }

        public async Task UpdateAvailableDates(Guid employeeId, IList<AvailableDateDto> availableDates)
        {
            try
            {
                var employee = await _employeeRepository.GetAsync(employeeId);
                if (employee is null)
                {
                    throw new InvalidOperationException($"Employee with id {employeeId} not found.");
                }
                var mappedAvailableDates = _mapper.Map<List<AvailableDate>>(availableDates);
                employee.AvailableDates = mappedAvailableDates;
                await _availableDateRepository.SaveChangesAsync();

                await _availableHourRepository.EmptyTableAsync();
                DateTime currentDate = DateTime.Today;
                for (int i = 0; i < 14; i++) // 14 dni, czyli 2 tygodnie
                {
                    var availableDate = await _availableDateRepository.GetByDayOfWeek(currentDate.DayOfWeek);
                    if (availableDate.IsOpen)
                    {
                        DateTime Interval = currentDate + availableDate.WorkingHours.StartTime;

                        while (Interval < currentDate + availableDate.WorkingHours.EndTime)
                        {
                            await _availableHourRepository.AddAsync(new AvailableHour(employee.Id, Interval));
                            Interval = Interval.AddMinutes(15);
                        }
                        currentDate = currentDate.AddDays(1);// Przejście do następnego dnia
                    }
                    else
                    {
                        currentDate = currentDate.AddDays(1);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public async Task UpdateAvailableHours(Guid employeeId,int duration, DateTime? selectedHour)
        {
            try
            {
                var availableHours = await GetAvailableHours(employeeId, selectedHour);
                if (availableHours is null)
                {
                    throw new Exception("Error getting availablehours");
                }
                TimeSpan durationAsTimeSpan = TimeSpan.FromMinutes(duration);
                var endTime = selectedHour.Value.TimeOfDay + durationAsTimeSpan;
                foreach (var availableHour in availableHours)
                {
                    if (availableHour.Hour.TimeOfDay >= selectedHour.Value.TimeOfDay && availableHour.Hour.TimeOfDay < endTime)
                    {
                        availableHour.IsAvailable = false;
                        _availableHourRepository.Update(availableHour);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public async Task<EmployeeDto> GetByUserId(string userId)
        {
            var employee = await _employeeRepository.GetByUserIdAsync(userId);
            if (employee is null)
            {
                throw new Exception($"employee with userId: {userId} wasnt found.");
            }
            var mappedemployee = _mapper.Map<EmployeeDto>(employee);

            return mappedemployee;
        }

        public async Task CreateForUser(Employee employee)
        {
            var employeeCheck = await _employeeRepository.GetByEmailAsync(employee.Email);
            if (employeeCheck is not null)
            {
                throw new Exception($"Employee with id: '{employee.Email}' already exists.");
            }
            await _employeeRepository.AddAsync(employee);
        }
    }
}

