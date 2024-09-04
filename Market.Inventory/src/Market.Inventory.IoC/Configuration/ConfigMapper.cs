using AutoMapper;
using Market.Inventory.IoC.Profiles;

namespace Market.Inventory.IoC.Configuration
{
    public class ConfigMapper
    {
        public static IMapper ConfigAppMapper()
        {
            var configMapper = new AutoMapper.MapperConfiguration(cfg => 
            {
                cfg.AddProfile(new ProductProfile());
            });

            return configMapper.CreateMapper();
        }
    }
}