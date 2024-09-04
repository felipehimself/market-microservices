using AutoMapper;
using Market.Inventory.Application.Dtos;
using Market.Inventory.Domain.Entities;
using Market.Inventory.Domain.Interfaces;
using Market.Inventory.Application.Interfaces;

namespace Market.Inventory.Application.Services
{
    public class ProductService(IProductRepository repository, IMapper mapper) : IProductService
    {
        private readonly IProductRepository _repository = repository;
        private readonly IMapper _mapper = mapper;


        public async Task<ProductReadDto> CreateProductAsync(ProductCreateDto productDto)
        {

        var entity = _mapper.Map<Product>(productDto);
           
        await _repository.CreateProductAsync(entity);
        await _repository.SaveChangesAsync();

        return  _mapper.Map<ProductReadDto>(entity);
           
        }

        public async Task<ProductReadDto> GetProductAsync(Guid id)
        {
            var entity = await _repository.GetProductAsync(id);

            return _mapper.Map<ProductReadDto>(entity);
        }

        public async Task<IEnumerable<ProductReadDto>> GetProductsAsync()
        {
            var products = await _repository.GetProductsAsync();

            return _mapper.Map<IEnumerable<ProductReadDto>>(products);

        }
    }
}