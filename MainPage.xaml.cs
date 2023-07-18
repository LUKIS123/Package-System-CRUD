using CommunityToolkit.Maui.Views;
using Package_System_CRUD.BusinessLogic;
using Package_System_CRUD.UserPages;
using Package_System_CRUD.UserPages.PopUps;

namespace Package_System_CRUD;

public partial class MainPage : ContentPage
{
    private readonly UserAuthenticationService _app;
    public string Username { get; private set; } = string.Empty;

    public MainPage(UserAuthenticationService app)
    {
        InitializeComponent();
        _app = app;
    }

    private async void OnCustomerLoginClicked(object sender, EventArgs e)
    {
        if (UsernameEntry.Text != null) Username = UsernameEntry.Text;

        var loginSuccessful = _app.AuthenticateCustomer(Username);
        if (loginSuccessful)
        {
            LoginInfoLbl.Text = "Logged in successfully!";
            LoginInfoLbl.TextColor = Colors.LightGreen;

            await Shell.Current.GoToAsync(nameof(CustomerPage));
        }
        else
        {
            LoginInfoLbl.Text = "Login Failed! Invalid Username!";
            LoginInfoLbl.TextColor = Colors.Red;
        }

        LoginInfoLbl.IsVisible = true;
    }

    private async void OnManufacturerLoginClicked(object sender, EventArgs e)
    {
        if (UsernameEntry.Text != null) Username = UsernameEntry.Text;
        var loginSuccessful = _app.AuthenticateManufacturer(Username);
        if (loginSuccessful)
        {
            LoginInfoLbl.Text = "Logged in successfully!";
            LoginInfoLbl.TextColor = Colors.LightGreen;

            await Shell.Current.GoToAsync(nameof(ManufacturerPage));
        }
        else
        {
            LoginInfoLbl.Text = "Login Failed! Invalid Username!";
            LoginInfoLbl.TextColor = Colors.Red;
        }

        LoginInfoLbl.IsVisible = true;
    }

    private async void OnEmployeeLoginClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync(nameof(EmployeePage));
    }

    private async void OnRegisterDialogPopup(object sender, EventArgs e)
    {
        var popup = new RegisterPopUp();
        var result = await this.ShowPopupAsync(popup);

        var registrationSuccessful = false;
        if (result is not (bool and true)) return;
        if (popup.Username != null && popup.UserTypeCustomer.HasValue)
        {
            RegistrationInfoLbl.Text = "Registered successfully! Please Login!";
            RegistrationInfoLbl.TextColor = Colors.LightGreen;

            registrationSuccessful = _app.RegisterNewUser(popup.Username, popup.UserTypeCustomer.Value);
        }

        if (!registrationSuccessful)
        {
            RegistrationInfoLbl.Text = "Registration Failed! Username must be unique";
            RegistrationInfoLbl.TextColor = Colors.Red;
        }

        RegistrationInfoLbl.IsVisible = true;
    }
}