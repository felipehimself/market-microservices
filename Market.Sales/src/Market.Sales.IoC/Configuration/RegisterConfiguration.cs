
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

        public static void ConfigRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            string rabbitMQHost = configuration["RabbitMQHost"]!;
            string rabbitMQPort = configuration["RabbitMQPort"]!;

            var rabbitHost = "amqp://" + rabbitMQHost + ":" + int.Parse(rabbitMQPort); ;
            services.AddMassTransit(bussConfigurator =>
            {
                bussConfigurator.UsingRabbitMq((ctx, cfg) =>
                {
                    // cfg.Message<SaleCreatedEvent>(x =>
                    // {
                    //     x.SetEntityName("sale-created-event-exchange");
                    // });

                    cfg.Host(rabbitHost, h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
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
            // services.AddSingleton(typeof(ILoggerService<>), typeof(LoggerService<>));
            return services;
        }

    }
}