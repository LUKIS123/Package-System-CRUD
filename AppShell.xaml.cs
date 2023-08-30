using Package_System_CRUD.UserPages;
using Package_System_CRUD.UserPages.Management;
using Package_System_CRUD.UserPages.Shop;

namespace Package_System_CRUD;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(CustomerPage), typeof(CustomerPage));
        Routing.RegisterRoute(nameof(ManufacturerPage), typeof(ManufacturerPage));
        Routing.RegisterRoute(nameof(EmployeePage), typeof(EmployeePage));

        Routing.RegisterRoute(nameof(ManufacturerOrderManagement), typeof(ManufacturerOrderManagement));
        Routing.RegisterRoute(nameof(EmployeeOrderManagement), typeof(EmployeeOrderManagement));

        Routing.RegisterRoute(nameof(ShopPage), typeof(ShopPage));
        Routing.RegisterRoute(nameof(ProductSelectionPage), typeof(ProductSelectionPage));
        Routing.RegisterRoute(nameof(ShoppingCartPage), typeof(ShoppingCartPage));
        Routing.RegisterRoute(nameof(AddNewProductPage), typeof(AddNewProductPage));
    }
}