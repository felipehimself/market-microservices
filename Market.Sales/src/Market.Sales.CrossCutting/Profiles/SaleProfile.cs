using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Market.Sales.Application.Dtos;
using Market.Sales.Domain.Entities;

namespace Market.Sales.CrossCutting.Profiles
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