﻿using AutoMapper;
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
        public void Create(Guid Id,string name, decimal price, int duration, Guid employeeId)
        {
            var service = new Service(name, price, duration, employeeId);
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
