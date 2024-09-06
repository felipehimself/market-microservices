using AutoMapper;
using Market.Inventory.Application.Dtos;
using Market.Inventory.Domain.Entities;
using Market.Inventory.Domain.Interfaces;
using Market.Inventory.Application.Interfaces;
using Market.Inventory.CrossCutting.Logger;

namespace Market.Inventory.Application.Services
{
    public class ProductService(IProductRepository repository, IMapper mapper, ILoggerService<ProductService> logger) : IProductService
    {
        private readonly IProductRepository _repository = repository;
        private readonly IMapper _mapper = mapper;
        private readonly ILoggerService<ProductService> _logger = logger;


        public ProductReadDto CreateProduct(ProductCreateDto productDto)
        {

            _logger.LogInformation("Creating Product");

            var entity = _mapper.Map<Product>(productDto);

            _repository.CreateProduct(entity);
            _repository.SaveChanges();

            return _mapper.Map<ProductReadDto>(entity);

        }

        public ProductReadDto GetProduct(Guid id)
        {
            _logger.LogInformation("Getting Product");

            var entity = _repository.GetProduct(id);

            if (entity == null) _logger.LogInformation("Not found, product is null");

            return _mapper.Map<ProductReadDto>(entity);
        }

        public IEnumerable<ProductReadDto> GetProducts()
        {
            _logger.LogInformation("Getting Products");

            var products = _repository.GetProducts();

            return _mapper.Map<IEnumerable<ProductReadDto>>(products);

        }

        public void UpdateProductInInventory(Guid id, int quantity)
        {
            _repository.UpdateProductInInventory(id, quantity);
            _repository.SaveChanges();

        }
    }
}