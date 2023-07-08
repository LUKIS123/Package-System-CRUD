using CommunityToolkit.Maui.Views;

namespace Package_System_CRUD.UserPages.PopUps;

public partial class RegisterPopUp : Popup
{
    public RegisterPopUp()
    {
        InitializeComponent();
    }

    void OnOKButtonClicked(object? sender, EventArgs e) => Close();
}