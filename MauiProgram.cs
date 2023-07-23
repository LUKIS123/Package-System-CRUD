using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Package_System_CRUD.BusinessLogic;
using Package_System_CRUD.BusinessLogic.Config;
using Package_System_CRUD.BusinessLogic.Data;
using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Repositories;
using Package_System_CRUD.BusinessLogic.Services;
using Package_System_CRUD.UserPages;
using Package_System_CRUD.UserPages.Management;

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

        builder.Configuration.AddConfiguration(
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build()
        );
        builder.Services.AddSingleton<ConfigurationProperties>();

        builder.Services.AddSingleton<IModelRepository<Customer>, CustomerRepository>();
        builder.Services.AddSingleton<IModelRepository<Manufacturer>, ManufacturerRepository>();
        builder.Services.AddSingleton<IModelRepository<Product>, ProductRepository>();
        builder.Services.AddSingleton<IModelRepository<Order>, OrderRepository>();

        builder.Services.AddSingleton<UserAuthenticationService>();
        builder.Services.AddSingleton<IModelService<Customer>, CustomerService>();
        builder.Services.AddSingleton<IModelService<Manufacturer>, ManufacturerService>();
        builder.Services.AddSingleton<IModelService<Product>, ProductService>();
        builder.Services.AddSingleton<IModelServiceExtended<Order>, OrderService>();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<CustomerPage>();
        builder.Services.AddSingleton<ManufacturerPage>();
        builder.Services.AddSingleton<EmployeePage>();
        builder.Services.AddTransient<EmployeeOrderManagement>();
        builder.Services.AddTransient<ManufacturerOrderManagement>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}