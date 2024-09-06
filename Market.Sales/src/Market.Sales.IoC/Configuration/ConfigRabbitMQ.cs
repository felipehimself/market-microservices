using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Market.Sales.IoC.Configuration
{
    public static class ConfigRabbitMQ
    {
        public static void ConfigRabbitMQServices(this IServiceCollection services, IConfiguration configuration)
        {
            string rabbitMQHost = configuration["RabbitMQHost"]!;
            string rabbitMQPort = configuration["RabbitMQPort"]!;

            var rabbitHost = "amqp://" + rabbitMQHost + ":" + int.Parse(rabbitMQPort);;
            services.AddMassTransit(bussConfigurator =>
            {
                bussConfigurator.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(rabbitHost, h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ConfigureEndpoints(ctx);
                });
            });
        }
    }
}