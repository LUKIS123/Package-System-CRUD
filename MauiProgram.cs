using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Package_System_CRUD.BusinessLogic.Config;
using Package_System_CRUD.BusinessLogic.Data;
using Package_System_CRUD.BusinessLogic.DateTimeProvider;
using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Repositories;
using Package_System_CRUD.BusinessLogic.Services.Authentication;
using Package_System_CRUD.BusinessLogic.Services.Database;
using Package_System_CRUD.BusinessLogic.Services.Database.Orders;
using Package_System_CRUD.BusinessLogic.Services.Database.Products;
using Package_System_CRUD.BusinessLogic.Services.ShoppingCart;
using Package_System_CRUD.UserPages;
using Package_System_CRUD.UserPages.Authentication;
using Package_System_CRUD.UserPages.Management;
using Package_System_CRUD.UserPages.Shop;

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
        builder.Services.AddDateTimeProvider(builder.Configuration);

        builder.Services.AddSingleton<IModelRepository<Customer>, CustomerRepository>();
        builder.Services.AddSingleton<IModelRepository<Manufacturer>, ManufacturerRepository>();
        builder.Services.AddSingleton<IModelRepository<Employee>, EmployeeRepository>();
        builder.Services.AddSingleton<IModelRepository<Product>, ProductRepository>();
        builder.Services.AddSingleton<IModelRepository<Order>, OrderRepository>();

        builder.Services.AddSingleton<IUserAuthenticationService, UserAuthenticationService>();
        builder.Services.AddSingleton<IShopCartService, ShopCartService>();

        builder.Services.AddSingleton<IModelService<Customer>, CustomerService>();
        builder.Services.AddSingleton<IModelService<Manufacturer>, ManufacturerService>();
        builder.Services.AddSingleton<IModelService<Employee>, EmployeeService>();
        builder.Services.AddSingleton<IProductService<Product>, ProductService>();
        builder.Services.AddSingleton<IOrderService<Order>, OrderService>();

        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<CustomerPage>();
        builder.Services.AddSingleton<ManufacturerPage>();
        builder.Services.AddSingleton<EmployeePage>();
        builder.Services.AddSingleton<ShopPage>();

        builder.Services.AddTransient<EmployeeOrderManagement>();
        builder.Services.AddTransient<ManufacturerOrderManagement>();
        builder.Services.AddTransient<ProductSelectionPage>();
        builder.Services.AddTransient<ShoppingCartPage>();
        builder.Services.AddTransient<AddNewProductPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}