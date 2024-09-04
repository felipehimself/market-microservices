using AutoMapper;
using Market.Sales.IoC.Profiles;

namespace Market.Sales.IoC.Configuration
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