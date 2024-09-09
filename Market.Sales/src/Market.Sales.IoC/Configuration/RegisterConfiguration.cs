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
        public static IServiceCollection ConfigAppConnectionString(this IServiceCollection services, IConfiguration config, bool isProduction)
        {

            if (isProduction)
            {
                var connectionString = config.GetConnectionString("MktIneventoryConnectionString");
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

        public static void ConfigRabbitMQ(this IServiceCollection services, IConfiguration configuration, bool isProduction)
        {
            var rabbitMQHost = "";

            if (isProduction)
            {
                rabbitMQHost = Environment.GetEnvironmentVariable("RabbitMQHost")!;
            }
            else
            {
                rabbitMQHost = configuration["RabbitMQHost"]!;
            }


            string rabbitMQPort = configuration["RabbitMQPort"]!;
            string rabbitMQUser = configuration["RabbitMQUser"]!;
            string RabbitMQPassword = configuration["RabbitMQPassword"]!;

            var host = "amqp://" + rabbitMQHost + ":" + int.Parse(rabbitMQPort);

            Console.WriteLine("HOST: " + host);

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