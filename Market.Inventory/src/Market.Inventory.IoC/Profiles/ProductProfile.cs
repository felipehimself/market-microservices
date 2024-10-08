using AutoMapper;
using Market.Inventory.Application.Dtos;
using Market.Inventory.Domain.Entities;

namespace Market.Inventory.IoC.Profiles
{
    public class ProductProfile : Profile
    {
        
        public ProductProfile()
        {
            CreateMap<ProductCreateDto, Product>();
            CreateMap<Product, ProductReadDto>();
        }
    }
}