using Package_System_CRUD.UserPages;

namespace Package_System_CRUD;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(CustomerPage), typeof(CustomerPage));
        Routing.RegisterRoute(nameof(ManufacturerPage), typeof(ManufacturerPage));
        Routing.RegisterRoute(nameof(EmployeePage), typeof(EmployeePage));
    }
}