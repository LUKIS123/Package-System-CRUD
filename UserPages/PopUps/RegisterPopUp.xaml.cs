using CommunityToolkit.Maui.Views;

namespace Package_System_CRUD.UserPages.PopUps;

public partial class RegisterPopUp : Popup
{
    public string? Username { get; private set; }
    public bool? UserTypeCustomer { get; private set; }
    public bool IsCustomerChecked { get; set; }

    public RegisterPopUp()
    {
        InitializeComponent();
        CustomerCheckBox.IsChecked = true;
    }

    private void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        if (UsernameEntry.Text != null) Username = UsernameEntry.Text;
        UserTypeCustomer = IsCustomerChecked;
        Close(true);
    }

    private void OnManufacturerCheckBoxSelect(object? sender, CheckedChangedEventArgs e)
    {
        if (!IsCustomerChecked)
        {
            return;
        }

        CustomerCheckBox.IsChecked = false;
        ManufacturerCheckBox.IsChecked = true;
        IsCustomerChecked = false;
    }

    private void OnCustomerCheckBoxSelect(object? sender, CheckedChangedEventArgs e)
    {
        if (IsCustomerChecked)
        {
            return;
        }

        ManufacturerCheckBox.IsChecked = false;
        CustomerCheckBox.IsChecked = true;
        IsCustomerChecked = true;
    }

    private void OnCancelButtonClicked(object? sender, EventArgs e) => Close(false);
}