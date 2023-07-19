using Microsoft.Extensions.Configuration;

namespace Package_System_CRUD.BusinessLogic.Config
{
    public class ConfigurationProperties
    {
        public ConfigurationProperties(IConfiguration config)
        {
            CustomerPageItemCount = Convert.ToInt32(config.GetSection("CustomerPage:ItemCount").Value);
            ManufacturerPageItemCount = Convert.ToInt32(config.GetSection("ManufacturerPage:ItemCount").Value);
        }

        public int CustomerPageItemCount { get; init; }
        public int ManufacturerPageItemCount { get; init; }
    }
}