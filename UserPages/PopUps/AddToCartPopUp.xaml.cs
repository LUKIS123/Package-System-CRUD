using CommunityToolkit.Maui.Views;
using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.UserPages.PopUps;

public partial class AddToCartPopUp : Popup
{
    public int ProductQuantity { get; private set; }

    public AddToCartPopUp(Product product)
    {
        InitializeComponent();
        ProductOverviewLbl.Text = product.Overview;
    }

    private void AddToCartButtonClicked(object? sender, EventArgs e)
    {
        var input = QuantityEntry.Text;
        if (input is null) return;
        if (int.TryParse(input, out var value))
        {
            ProductQuantity = value;
            Close(true);
        }
    }

    private void OnCancelButtonClicked(object? sender, EventArgs e) => Close(false);
}