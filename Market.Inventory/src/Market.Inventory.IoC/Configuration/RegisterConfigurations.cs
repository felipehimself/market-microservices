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
// using Market.Inventory.Application.Events;
using Market.FakeForMassTransit.Application.Events;


namespace Market.Inventory.IoC.Configuration
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
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;

        }

        public static void ConfigRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            string rabbitMQHost = configuration["RabbitMQHost"]!;
            string rabbitMQPort = configuration["RabbitMQPort"]!;

            var rabbitHost = "amqp://" + rabbitMQHost + ":" + int.Parse(rabbitMQPort); ;
            services.AddMassTransit(bussConfigurator =>
            {
                bussConfigurator.AddConsumer<SaleCreatedEventConsumer>();

                bussConfigurator.UsingRabbitMq((ctx, cfg) =>
                {

                    // cfg.Message<SaleCreatedEvent>(x =>
                    //  {
                    //      x.SetEntityName("sale-created-event-exchange");
                    //  });


                    cfg.Host(rabbitHost, h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    // Configure the endpoint to consume messages
                    cfg.ReceiveEndpoint("sale-created-queue", e =>
                    {
                        e.ConfigureConsumer<SaleCreatedEventConsumer>(ctx);
                    });


                    // cfg.ConfigureEndpoints(ctx);
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