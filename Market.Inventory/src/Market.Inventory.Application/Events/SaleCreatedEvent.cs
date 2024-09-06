namespace Market.FakeForMassTransit.Application.Events
{
    public record SaleCreatedEvent(Guid ProductId, int Quantity);

}