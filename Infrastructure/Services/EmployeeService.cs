using AutoMapper;
using Bulldog.Core.Domain;
using Bulldog.Core.Repositories;
using Bulldog.Infrastructure.Commands.AvailableDates;
using Bulldog.Infrastructure.Services.DTO;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IList<AvailableHour>> GetAvailableHours(Guid employeeId, DateTime date)
        {
            return await _availableHourRepository.GetAvailableForDayAsync(employeeId, date);
        }

        public async Task<EmployeeDto> GetById(Guid Id)
        {
            var employee = await _employeeRepository.GetAsync(Id);
            if (employee != null)
            {
                var employeeToReturn = _mapper.Map<EmployeeDto>(employee);
                return employeeToReturn;
            }
            throw new Exception($"Error while getting by Id: {Id}");

        }

        public async Task<IList<EmployeeDto>> GetByServiceId(Guid Id)
        {
            var employees = await _employeeRepository.GetEmployeesForServiceIdAsync(Id);
            if (employees.Count > 0)
            {
                var employeesDto = _mapper.Map<IList<EmployeeDto>>(employees);
                return employeesDto;
            }
            throw new Exception($"No employees found for service with id: {Id}");

        }

        public async Task<EmployeeDto> GetByEmail(string email)
        {
            var employee = await _employeeRepository.GetByEmailAsync(email);
            if (employee != null)
            {
                var mappedemployee = _mapper.Map<EmployeeDto>(employee);
                return mappedemployee;
            }
            throw new Exception($"Employee with email: {email} wasnt found.");
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
                if (employee == null)
                {
                    throw new InvalidOperationException($"Employee with id {employeeId} not found.");
                }
                else
                {
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
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


    }
}

