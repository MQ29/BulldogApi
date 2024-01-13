using AutoMapper;
using Bulldog.Core.Domain;
using Bulldog.Core.Repositories;
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

        public EmployeeService(IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }
        public async Task Create(Guid userId)
        {
            // Tutaj możesz wykorzystać identyfikatory do utworzenia obiektu Reservation.

            // Następnie tworzysz rezerwację:
            var employee = new Employee(userId);
            
            // Dodaj do repozytorium rezerwacji:
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
