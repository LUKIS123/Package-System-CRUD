using CommunityToolkit.Maui.Views;
using Package_System_CRUD.BusinessLogic.Services.Authentication;
using Package_System_CRUD.UserPages.PopUps;

namespace Package_System_CRUD.UserPages.Authentication;

[QueryProperty(nameof(Username), "Username")]
public partial class MainPage : ContentPage
{
    private readonly IUserAuthenticationService _userAuthenticationService;
    public string Username { get; set; } = string.Empty;

    public MainPage(IUserAuthenticationService userAuthenticationService)
    {
        InitializeComponent();
        _userAuthenticationService = userAuthenticationService;
    }

    private async void OnCustomerLoginClicked(object sender, EventArgs e)
    {
        if (UsernameEntry.Text != null) Username = UsernameEntry.Text;

        var loginSuccessful = _userAuthenticationService.AuthenticateCustomer(Username);
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

        var loginSuccessful = _userAuthenticationService.AuthenticateManufacturer(Username);
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
        if (UsernameEntry.Text != null) Username = UsernameEntry.Text;

        var loginSuccessful = _userAuthenticationService.AuthenticateEmployee(Username);
        if (loginSuccessful)
        {
            LoginInfoLbl.Text = "Logged in successfully!";
            LoginInfoLbl.TextColor = Colors.LightGreen;

            await Shell.Current.GoToAsync(nameof(EmployeePage));
        }
        else
        {
            LoginInfoLbl.Text = "Login Failed! Invalid Username!";
            LoginInfoLbl.TextColor = Colors.Red;
        }

        LoginInfoLbl.IsVisible = true;
    }

    private async void OnRegisterDialogPopup(object sender, EventArgs e)
    {
        var popup = new RegisterPopUp();
        var result = await this.ShowPopupAsync(popup);

        var registrationSuccessful = false;
        if (result is not (bool and true)) return;
        if (popup.Username != null)
        {
            registrationSuccessful = _userAuthenticationService.RegisterNewUser(popup.Username, popup.User);
            RegistrationInfoLbl.Text = "Registered successfully! Please Login!";
            RegistrationInfoLbl.TextColor = Colors.LightGreen;
        }

        if (!registrationSuccessful)
        {
            RegistrationInfoLbl.Text = "Registration Failed! Username must be unique";
            RegistrationInfoLbl.TextColor = Colors.Red;
        }

        RegistrationInfoLbl.IsVisible = true;
    }
}