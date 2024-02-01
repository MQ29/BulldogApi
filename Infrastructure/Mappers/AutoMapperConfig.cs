using AutoMapper;
using Bulldog.Core.Domain;
using Bulldog.Infrastructure.Commands.AvailableDates;
using Bulldog.Infrastructure.Services.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Bulldog.Infrastructure.Mappers
{
    public class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<Service, ServiceDto>();
                cfg.CreateMap<Employee, EmployeeDto>();
                cfg.CreateMap<EmployeeDto, Employee>();
                cfg.CreateMap<AvailableDate, AvailableDateDto>();
                cfg.CreateMap<AvailableDateDto, AvailableDate>();
                cfg.CreateMap<CreateAvailableDate, AvailableDate>();
                cfg.CreateMap<AvailableDate, CreateAvailableDate>();
                cfg.CreateMap<ReservationDto, Reservation>();
                cfg.CreateMap<Reservation, ReservationDto>();
                cfg.CreateMap<Company, CompanyDto>();
                cfg.CreateMap<CompanyDto, Company>();
            });
            return configuration.CreateMapper();
        }
    }
}

