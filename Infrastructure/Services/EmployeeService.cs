using AutoMapper;
using Bulldog.Core.Domain;
using Bulldog.Core.Repositories;
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
    }
}
