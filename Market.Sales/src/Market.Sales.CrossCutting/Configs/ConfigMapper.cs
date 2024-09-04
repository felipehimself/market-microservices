using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Market.Sales.CrossCutting.Profiles;

namespace Market.Sales.CrossCutting.Configs
{
    public class ConfigMapper
    {
        public static IMapper ConfigAppMapper()
        {
            var configMapper = new AutoMapper.MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new SaleProfile());
            });

            return configMapper.CreateMapper();
        }
    }
}