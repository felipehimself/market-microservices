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


        public async Task<ProductReadDto> CreateProductAsync(ProductCreateDto productDto)
        {

        _logger.LogInformation("Creating Product");

        var entity = _mapper.Map<Product>(productDto);
           
        await _repository.CreateProductAsync(entity);
        await _repository.SaveChangesAsync();

        return  _mapper.Map<ProductReadDto>(entity);
           
        }

        public async Task<ProductReadDto> GetProductAsync(Guid id)
        {
            _logger.LogInformation("Getting Product");

            var entity = await _repository.GetProductAsync(id);

            if (entity == null) _logger.LogInformation("Not found, product is null");

            return _mapper.Map<ProductReadDto>(entity);
        }

        public async Task<IEnumerable<ProductReadDto>> GetProductsAsync()
        {   
            _logger.LogInformation("Getting Products");

            var products = await _repository.GetProductsAsync();

            return _mapper.Map<IEnumerable<ProductReadDto>>(products);

        }
    }
}