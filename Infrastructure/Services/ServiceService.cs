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
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        public ServiceService(Core.Repositories.IServiceRepository serviceRepository, IMapper mapper)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
        }
        public void Create(string name, decimal price, int duration)
        {
            var service = _serviceRepository.Get(name);
            if (service != null)
                throw new Exception($"Service:{name} already exists");
            service = new Service(name, price, duration);
            _serviceRepository.Add(service);
        }

        public ServiceDto Get(string name)
        {
            var service = _serviceRepository.Get(name);
            return _mapper.Map<Service, ServiceDto>(service);
        }

        public ServiceDto Get(Guid employeeId)
        {
            var service = _serviceRepository.GetByEmployeeId(employeeId);
            return _mapper.Map<ServiceDto>(service);
        }
    }
}
