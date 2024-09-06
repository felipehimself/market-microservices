using Market.FakeForMassTransit.Application.Events;
using Market.Sales.Application.Interfaces;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Market.Sales.Application.Providers
{
    public class RabbitMQProvider(IPublishEndpoint publish, ILogger<RabbitMQProvider> logger) : IRabbitMQProvider
    {

        private readonly IPublishEndpoint _publish = publish;
        private readonly ILogger<RabbitMQProvider> _logger = logger;

        public async Task PublishSaleCreated(SaleCreatedEvent message)
        {
            _logger.LogInformation("Publishing sale created event: {@message}", message);
            await _publish.Publish<SaleCreatedEvent>(message);
        }
    }
}