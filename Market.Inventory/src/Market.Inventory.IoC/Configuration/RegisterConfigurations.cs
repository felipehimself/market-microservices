using MassTransit;
using Market.Inventory.Application.Interfaces;
using Market.Inventory.Application.Services;
using Market.Inventory.CrossCutting.Logger;
using Market.Inventory.Domain.Interfaces;
using Market.Inventory.Infrastructure.Context;
using Market.Inventory.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Market.Inventory.Application.Consumers;

namespace Market.Inventory.IoC.Configuration
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
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;

        }

        public static void ConfigRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {

            var rabbitMQHost = Environment.GetEnvironmentVariable("RabbitMQHost") ?? configuration["RabbitMQHost"]!;
            var rabbitMQPort = Environment.GetEnvironmentVariable("RabbitMQPort") ?? configuration["RabbitMQPort"]!;
            var rabbitMQUser = Environment.GetEnvironmentVariable("RabbitMQUser") ?? configuration["RabbitMQUser"]!;
            var RabbitMQPassword = Environment.GetEnvironmentVariable("RabbitMQPassword") ?? configuration["RabbitMQPassword"]!;

            var host = "amqp://" + rabbitMQHost + ":" + int.Parse(rabbitMQPort);

            services.AddMassTransit(bussConfigurator =>
            {
                bussConfigurator.AddConsumer<SaleCreatedEventConsumer>();

                bussConfigurator.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(host, h =>
                    {
                        h.Username(rabbitMQUser);
                        h.Password(RabbitMQPassword);
                    });

                    cfg.ReceiveEndpoint("sale-created-queue", e =>
                    {
                        e.ConfigureConsumer<SaleCreatedEventConsumer>(ctx);
                    });


                    cfg.ConfigureEndpoints(ctx);
                });
            });
        }
        public static IServiceCollection ConfigAppServices(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddSingleton(typeof(ILoggerService<>), typeof(LoggerService<>));
            return services;
        }

    }
}