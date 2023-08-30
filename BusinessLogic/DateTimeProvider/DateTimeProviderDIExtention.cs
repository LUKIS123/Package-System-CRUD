using Microsoft.Extensions.Configuration;

namespace Package_System_CRUD.BusinessLogic.DateTimeProvider
{
    public static class DateTimeProviderDIExtention
    {
        public static IServiceCollection AddDateTimeProvider(
            this IServiceCollection serviceCollection,
            IConfiguration config
        )
        {
            var dateTimeProviderImplementationType =
                config.GetSection("DateTimeProvider:ImplementationType").Value ?? "Default";


            if (dateTimeProviderImplementationType == "File")
            {
                serviceCollection.AddSingleton<IDateTimeProvider, FileDateTimeProvider>();
            }
            else
            {
                serviceCollection.AddSingleton<IDateTimeProvider, DefaultDateTimeProvider>();
            }

            return serviceCollection;
        }
    }
}