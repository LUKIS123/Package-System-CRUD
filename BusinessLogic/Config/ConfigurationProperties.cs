using Microsoft.Extensions.Configuration;

namespace Package_System_CRUD.BusinessLogic.Config
{
    public class ConfigurationProperties
    {
        public ConfigurationProperties(IConfiguration config)
        {
            CustomerPageItemCount = Convert.ToInt32(config.GetSection("CustomerPage:ItemCount").Value);
            ManufacturerPageItemCount = Convert.ToInt32(config.GetSection("ManufacturerPage:ItemCount").Value);
            EmployeePageItemCount = Convert.ToInt32(config.GetSection("EmployeePage:ItemCount").Value);
            ShopPageItemCount = Convert.ToInt32(config.GetSection("Shop:ItemCount").Value);
        }

        public int CustomerPageItemCount { get; init; }
        public int ManufacturerPageItemCount { get; init; }
        public int EmployeePageItemCount { get; init; }
        public int ShopPageItemCount { get; init; }
    }
}