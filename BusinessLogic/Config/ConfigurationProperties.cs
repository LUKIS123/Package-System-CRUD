using Microsoft.Extensions.Configuration;

namespace Package_System_CRUD.BusinessLogic.Config
{
    public class ConfigurationProperties
    {
        public ConfigurationProperties(IConfiguration config)
        {
            CustomerPageItemCount = TryParseInt(config.GetSection("CustomerPage:ItemCount").Value);
            ManufacturerPageItemCount = TryParseInt(config.GetSection("ManufacturerPage:ItemCount").Value);
            EmployeePageItemCount = TryParseInt(config.GetSection("EmployeePage:ItemCount").Value);
            ShopPageItemCount = TryParseInt(config.GetSection("Shop:ItemCount").Value);

            var dateTimeProviderFilePath = config.GetSection("DateTimeProvider:FilePath").Value;
            FileDateTimeProviderSourcePath = dateTimeProviderFilePath ?? string.Empty;
        }

        public int CustomerPageItemCount { get; init; }
        public int ManufacturerPageItemCount { get; init; }
        public int EmployeePageItemCount { get; init; }
        public int ShopPageItemCount { get; init; }
        public string FileDateTimeProviderSourcePath { get; init; }

        private int TryParseInt(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return 0;
            }

            return int.TryParse(input, out var value) ? value : 0;
        }
    }
}