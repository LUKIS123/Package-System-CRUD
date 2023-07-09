using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Package_System_CRUD.BusinessLogic.Data;
using Package_System_CRUD.BusinessLogic.Repositories;
using Package_System_CRUD.BusinessLogic.Services;
using Package_System_CRUD.BusinessLogic.UserApps;

namespace Package_System_CRUD;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddDbContext<AppDbContext>(opt =>
            opt.UseSqlite(
                $"Data Source={Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PackageSystemDB.db")}"
            )
        );

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<ApplicationFlow>();

        builder.Services.AddSingleton<CustomerAppController>();
        builder.Services.AddSingleton<EmployeeAppController>();
        builder.Services.AddSingleton<ManufacturerAppController>();

        builder.Services.AddSingleton<CustomerService>();
        builder.Services.AddSingleton<ManufacturerService>();
        builder.Services.AddSingleton<OrderService>();
        builder.Services.AddSingleton<ProductService>();

        builder.Services.AddSingleton<CustomerRepository>();
        builder.Services.AddSingleton<ManufacturerRepository>();
        builder.Services.AddSingleton<OrderRepository>();
        builder.Services.AddSingleton<ProductRepository>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}