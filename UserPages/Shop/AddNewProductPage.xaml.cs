using Package_System_CRUD.BusinessLogic;
using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Services;

namespace Package_System_CRUD.UserPages.Shop;

public partial class AddNewProductPage : ContentPage
{
    private readonly IProductService<Product> _productService;
    private readonly UserAuthenticationService _userAuthenticationService;

    public AddNewProductPage(
        IProductService<Product> productService,
        UserAuthenticationService userAuthenticationService
    )
    {
        InitializeComponent();
        _productService = productService;
        _userAuthenticationService = userAuthenticationService;
    }

    private async void AddProductButtonClicked(object? sender, EventArgs e)
    {
        var name = NameEntry.Text;
        var description = DescriptionEntry.Text;
        decimal price = -1;
        if (decimal.TryParse(PriceEntry.Text, out var value))
        {
            price = value;
        }

        if (name is not null && description is not null && price != -1)
        {
            if (sender is Button btn) btn.IsEnabled = false;

            _productService.AddToDatabase(new Product
            {
                ManufacturerId = _userAuthenticationService.LoggedUserId,
                Name = name,
                Description = description,
                Price = price,
            });

            InfoLbl.Text = "Success!";
            InfoLbl.TextColor = Colors.LightGreen;
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            InfoLbl.Text = "Could not add Product! Check input for errors!";
            InfoLbl.TextColor = Colors.Red;
        }
    }

    private async void OnCancelButtonClicked(object? sender, EventArgs e)
    {
        if (sender is Button btn) btn.IsEnabled = false;
        await Shell.Current.GoToAsync("..");
    }
}