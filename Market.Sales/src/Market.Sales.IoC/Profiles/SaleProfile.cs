using AutoMapper;
using Market.Sales.Application.Dtos;
using Market.Sales.Domain.Entities;

namespace Market.Sales.IoC.Profiles
{
    public class SaleProfile : Profile
    {
        public SaleProfile()
        {
            CreateMap<SaleCreateDto, Sale>();
            CreateMap<Sale, SaleReadDto>();
        }
    }
}