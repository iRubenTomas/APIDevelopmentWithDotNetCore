using CarInventory.Domain.Interfaces;
using CarInventory.Domain.Interfaces.Shared;
using CarInventory.Infrastructure.Data;
using CarInventory.Infrastructure.Repositories;
using CarInventory.Infrastructure.Repositories.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace CarInventory.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Configure Entity Framework Core
            services.AddDbContext<CarInventoryDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Register repositories and unit of work
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ICarRepository, CarRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<ISalesRecordRepository, SalesRecordRepository>();

            return services;
        }
    }
}
