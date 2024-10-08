using AutoMapper;
using Market.Sales.Application.Dtos;
using Market.FakeForMassTransit.Application.Events;
using Market.Sales.Application.Interfaces;
using Market.Sales.Domain.Entities;
using Market.Sales.Domain.Interfaces;

namespace Market.Sales.Application.Services
{
    public class SaleService(ISaleRepository repository, IMapper mapper, IRabbitMQProvider rabbitMQProvider) : ISaleService
    {
        private readonly ISaleRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly IRabbitMQProvider _rabbitMQProvider = rabbitMQProvider;

        public async Task<SaleReadDto> CreateSaleAsync(SaleCreateDto sale)
        {

            var entity = _mapper.Map<Sale>(sale);

            await _repository.CreateSaleAsync(entity);
            await _repository.SaveChangesAsync();

            var eventRequest = new SaleCreatedEvent(sale.ProductId, sale.Quantity);
            await _rabbitMQProvider.PublishSaleCreated(eventRequest);

            return _mapper.Map<SaleReadDto>(entity);

        }

        public async Task<SaleReadDto?> GetSaleAsync(Guid id)
        {
            var entity = await _repository.GetSaleAsync(id);

            return _mapper.Map<SaleReadDto>(entity);
        }
    }
}