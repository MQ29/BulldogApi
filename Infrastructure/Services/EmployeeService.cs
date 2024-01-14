﻿using AutoMapper;
using Bulldog.Core.Domain;
using Bulldog.Core.Repositories;
using Bulldog.Infrastructure.Migrations;
using Bulldog.Infrastructure.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAvailableDateRepository _availableDateRepository;

        public EmployeeService(IMapper mapper, IEmployeeRepository employeeRepository,
            IUserRepository userRepository, IAvailableDateRepository availableDateRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _userRepository = userRepository;
            _availableDateRepository = availableDateRepository;
        }

        public async Task AddAvailableDate(Guid Id, DateTime startTime, DateTime endTime,
            string title, string description, string color)
        {
            var employee = await _employeeRepository.GetAsync(Id);
            if (employee == null)
            {
                throw new InvalidOperationException($"Employee with id {Id} not found.");
            }
            var availableDate = new AvailableDate(employee, startTime, endTime, title, description, color);
            employee.AddAvailableDate(availableDate);
            await _availableDateRepository.AddAsync(availableDate);
            //await _employeeRepository.UpdateAsync(employee);
        }

        public async Task Create(Guid userId)
        {
            var user = await _userRepository.GetAsync(userId);
            var employee = await _employeeRepository.GetAsync(userId);
            if (employee != null)
            {
                throw new Exception($"Employee with id: '{userId}' already exists.");
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

        public async Task<IList<AvailableDateDto>> GetAvailableDates(Guid Id)
        {
            var availableDates = await _availableDateRepository.GetAsync(Id);
            var availableDatesToReturn = _mapper.Map<IList<AvailableDateDto>>(availableDates);
            return availableDatesToReturn;
        }//TODO: Validation

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

        public async Task RemoveAsync(Guid Id)
        {
            await _employeeRepository.RemoveAsync(Id);
        }


    }
}
