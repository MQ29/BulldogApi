using AutoMapper;
using Bulldog.Core.Domain;
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
            });
            return configuration.CreateMapper();
        }
    }
}

