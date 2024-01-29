using AutoMapper;
using Azure.Core.Pipeline;
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
    public class ServiceService : IServiceService
    {
        
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;

        public ServiceService(IServiceRepository serviceRepository, IMapper mapper, IEmployeeRepository employeeRepository)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        public async Task Create(Guid employeeId,string name, decimal price, int duration)
        {
            var employee = await _employeeRepository.GetAsync(employeeId);
            if (employee is null)
            {
                throw new Exception($"Employee with {employeeId} wasnt found.");
            }
            var service = new Service(name, price, duration, employee);
            await _serviceRepository.AddAsync(service);
        }

        public async Task<IList<ServiceDto>> GetByEmployeeIdAsync(Guid employeeId)
        {
            var employee = await _employeeRepository.GetAsync(employeeId);
            if (employee is null)
            {
                throw new Exception($"Employee with {employeeId} wasnt found.");
            }
            var services = await _serviceRepository.GetByEmployeeIdAsync(employeeId);
            if (services is null)
            {
                throw new Exception($"Error finding services for employee with Id:{employeeId}.");
            }
            var servicesDtos = _mapper.Map<IList<ServiceDto>>(services);
            return servicesDtos;
        }

        public async Task<IList<ServiceDto>> GetAll()
        {
            var services = await _serviceRepository.GetAllAsync();
            var serviceDtos = new List<ServiceDto>();
            foreach (var service in services)
            {
                var serviceDto = _mapper.Map<ServiceDto>(service);
                serviceDtos.Add(serviceDto);
            }
            return serviceDtos;
        }

        public async Task RemoveAsync(Guid Id)
        {
            await _serviceRepository.RemoveAsync(Id);
        }

        public async Task<ServiceDto> GetAsync(Guid Id)
        {
            var service = await _serviceRepository.GetByIdAsync(Id);
            if (service == null)
            {
                throw new Exception($"Service with Id: {Id} wasnt found.");
            }
            var serviceDto = _mapper.Map<ServiceDto>(service);
            return serviceDto;
        }
    }
}
