using AutoMapper;
using Market.Inventory.CrossCutting.Profiles;

namespace Market.Inventory.CrossCutting.Configs
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