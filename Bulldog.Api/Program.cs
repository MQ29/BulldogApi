
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Bulldog.Core.Repositories;
using Bulldog.Infrastructure.EF;
using Bulldog.Infrastructure.Mappers;
using Bulldog.Infrastructure.Repositories;
using Bulldog.Infrastructure.Services;
using Infrastructure.Repositories;
using System.Security.Cryptography.X509Certificates;

namespace Bulldog.Api
{
    public class Program
    {
        public IConfigurationRoot Configuration { get; }
        public IContainer ApplicationContainer { get; private set; }
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped<IUserRepository, EFUserRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EFEmployeeRepository>();
            builder.Services.AddScoped<IServiceService, ServiceService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IServiceRepository, EFServiceRepository>();
            builder.Services.AddScoped<IReservationRepository, EFReservationRepository>();
            builder.Services.AddScoped<IAvailableDateRepository, EFAvailableDateRepository>();
            builder.Services.AddSingleton(AutoMapperConfig.Initialize());
            builder.Services.AddDbContext<BulldogDbContext>();

            var containerBuilder = new ContainerBuilder();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
