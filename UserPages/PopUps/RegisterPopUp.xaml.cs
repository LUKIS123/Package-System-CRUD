using CommunityToolkit.Maui.Views;
using Package_System_CRUD.BusinessLogic;

namespace Package_System_CRUD.UserPages.PopUps;

public partial class RegisterPopUp : Popup
{
    public string? Username { get; private set; }
    public UserType User { get; private set; }

    private readonly List<UserType> _userTypes = new() { UserType.Customer, UserType.Manufacturer, UserType.Employee };

    public RegisterPopUp()
    {
        InitializeComponent();

        UserTypePicker.ItemsSource = _userTypes;
        UserTypePicker.SelectedItem = _userTypes[0];

        User = UserType.Customer;
    }

    private void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        if (UsernameEntry.Text == null) return;
        Username = UsernameEntry.Text;
        Close(true);
    }

    private void OnSelectedIndexChanged(object? sender, EventArgs e)
    {
        var type = UserTypePicker.SelectedItem;

        if (type is UserType userType)
        {
            User = userType;
        }
    }

    private void OnCancelButtonClicked(object? sender, EventArgs e) => Close(false);
}