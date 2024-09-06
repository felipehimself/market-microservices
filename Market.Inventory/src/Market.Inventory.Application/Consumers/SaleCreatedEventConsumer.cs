
using Market.FakeForMassTransit.Application.Events;

using Market.Inventory.Application.Interfaces;
using Market.Inventory.CrossCutting.Logger;
using MassTransit;

namespace Market.Inventory.Application.Consumers
{
    public class SaleCreatedEventConsumer(IProductService productService, ILoggerService<SaleCreatedEventConsumer> logger) : IConsumer<SaleCreatedEvent>
    {

        private readonly IProductService _productService = productService;

        private readonly ILoggerService<SaleCreatedEventConsumer> _logger = logger;

        public Task Consume(ConsumeContext<SaleCreatedEvent> context)
        {
            var message = context.Message;

            _logger.LogInformation("Consuming product with ID: " + message.ProductId);
            _productService.UpdateProductInInventory(message.ProductId, message.Quantity);


            return Task.CompletedTask;

        }
    }
}