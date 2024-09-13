using MassTransit;
using Market.Sales.Application.Interfaces;
using Market.Sales.Application.Services;
using Market.Sales.CrossCutting.Logger;
using Market.Sales.Domain.Interfaces;
using Market.Sales.Infrastructure.Context;
using Market.Sales.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Market.Sales.Application.Providers;
using Market.FakeForMassTransit.Application.Events;

namespace Market.Sales.IoC.Configuration
{
    public static class RegisterConfigurations
    {
        public static IServiceCollection ConfigAppConnectionString(this IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("MktMSConnStr");

            if (!string.IsNullOrEmpty(connectionString))
            {
                services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connectionString));
            }
            else
            {
                services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMemDb"));
            }


            return services;

        }
        public static IServiceCollection ConfigAppDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<ISaleRepository, SaleRepository>();

            return services;

        }

        public static void ConfigRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMQHost = Environment.GetEnvironmentVariable("RabbitMQHost") ?? configuration["RabbitMQHost"]!;
            var rabbitMQPort = Environment.GetEnvironmentVariable("RabbitMQPort") ?? configuration["RabbitMQPort"]!;
            var rabbitMQUser = Environment.GetEnvironmentVariable("RabbitMQUser") ?? configuration["RabbitMQUser"]!;
            var RabbitMQPassword = Environment.GetEnvironmentVariable("RabbitMQPassword") ?? configuration["RabbitMQPassword"]!;

            var host = "amqp://" + rabbitMQHost + ":" + int.Parse(rabbitMQPort);

            Console.WriteLine("HOST ADDRESS: " + host);

            services.AddMassTransit(bussConfigurator =>
            {
                bussConfigurator.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(host, h =>
                    {
                        h.Username(rabbitMQUser);
                        h.Password(RabbitMQPassword);
                    });
                    cfg.ConfigureEndpoints(ctx);
                });
            });
        }
        public static IServiceCollection ConfigAppServices(this IServiceCollection services)
        {
            services
                .AddScoped<ISaleService, SaleService>()
                .AddScoped<IRabbitMQProvider, RabbitMQProvider>()
                .AddSingleton(typeof(ILoggerService<>), typeof(LoggerService<>));
            return services;
        }

    }
}