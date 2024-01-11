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
        public ServiceService(Core.Repositories.IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }
        public async Task Create(Guid Id,string name, decimal price, int duration, Guid employeeId)
        {
            var service = new Service(name, price, duration, employeeId);
            await _serviceRepository.AddAsync(service);
        }

        public async Task<ServiceDto> GetByEmployeeIdAsync(Guid employeeId)
        {
            var service = await _serviceRepository.GetByEmployeeIdAsync(employeeId);
            return _mapper.Map<ServiceDto>(service);
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
    }
}
