using Market.FakeForMassTransit.Application.Events;

namespace Market.Sales.Application.Interfaces
{
    public interface IRabbitMQProvider
    {
        Task PublishSaleCreated(SaleCreatedEvent message);
    }
}