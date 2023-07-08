using System.Runtime.CompilerServices;
using CommunityToolkit.Maui.Views;
using Package_System_CRUD.BusinessLogic;
using Package_System_CRUD.UserPages;
using Package_System_CRUD.UserPages.PopUps;

namespace Package_System_CRUD;

public partial class MainPage : ContentPage
{
    private readonly ApplicationFlow _app;

    public MainPage(ApplicationFlow app)
    {
        InitializeComponent();
        _app = app;
    }

    private async void OnCustomerLoginClicked(object sender, EventArgs e)
    {
        _app.Run();
        await AppShell.Current.GoToAsync(nameof(CustomerPage));

        // count++;
        // if (count == 1)
        //     LoginCustomerBtn.Text = $"Clicked {count} time";
        // else
        //     LoginCustomerBtn.Text = $"Clicked {count} times";
        //
        // SemanticScreenReader.Announce(LoginCustomerBtn.Text);
    }

    private async void OnManufacturerLoginClicked(object sender, EventArgs e)
    {
        await AppShell.Current.GoToAsync(nameof(ManufacturerPage));
    }

    private async void OnEmployeeLoginClicked(object sender, EventArgs e)
    {
        await AppShell.Current.GoToAsync(nameof(EmployeePage));
    }

    private async void OnRegisterDialogPopup(object sender, EventArgs e)
    {
        var popup = new RegisterPopUp();
        var result = await this.ShowPopupAsync(popup);
    }
}