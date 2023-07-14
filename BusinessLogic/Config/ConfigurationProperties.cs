using Microsoft.Extensions.Configuration;

namespace Package_System_CRUD.BusinessLogic.Config
{
    public class ConfigurationProperties
    {
        public ConfigurationProperties(IConfiguration config)
        {
            CustomerPageItemCount = Convert.ToInt32(config.GetSection("CustomerPage:ItemCount").Value);
        }

        public int CustomerPageItemCount { get; init; }
    }
}