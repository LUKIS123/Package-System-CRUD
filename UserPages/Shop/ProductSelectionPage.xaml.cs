using CommunityToolkit.Maui.Views;
using Package_System_CRUD.BusinessLogic.Enums;
using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Services.Authentication;
using Package_System_CRUD.BusinessLogic.Services.Database.Products;
using Package_System_CRUD.BusinessLogic.Services.ShoppingCart;
using Package_System_CRUD.UserPages.PopUps;

namespace Package_System_CRUD.UserPages.Shop;

[QueryProperty(nameof(Product), "Product")]
public partial class ProductSelectionPage : ContentPage
{
    private Product? _product;
    private readonly IShopCartService _shopCartService;
    private readonly IUserAuthenticationService _userAuthenticationService;
    private readonly IProductService<Product> _productService;

    public Product? Product
    {
        get => _product;
        set
        {
            _product = value;
            OnPropertyChanged();
        }
    }

    public ProductSelectionPage(
        IShopCartService shopCartService,
        IUserAuthenticationService userAuthenticationService,
        IProductService<Product> productService
    )
    {
        InitializeComponent();
        BindingContext = this;
        _shopCartService = shopCartService;
        _productService = productService;
        _userAuthenticationService = userAuthenticationService;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_userAuthenticationService.GetLoggedUserType() != UserType.Customer)
        {
            BuyButton.IsEnabled = false;
            BuyButton.TextColor = Colors.Grey;
        }

        if (_userAuthenticationService.GetLoggedUserType() == UserType.Manufacturer)
        {
            Application.Current?.Dispatcher.Dispatch(() =>
            {
                var btn = new Button
                {
                    Text = "Remove Item",
                };

                btn.Clicked += (sender, args) =>
                {
                    if (_product != null) _productService.RemoveFromDatabase(_product);
                    if (sender is Button btn) btn.IsEnabled = false;
                    Shell.Current.GoToAsync("..");
                };

                StackLayout.Add(btn);
            });
        }
    }

    private async void OnReturnButtonClicked(object? sender, EventArgs e)
    {
        if (sender is Button btn) btn.IsEnabled = false;
        await Shell.Current.GoToAsync("..");
    }

    private async void BuyProductButtonClicked(object? sender, EventArgs e)
    {
        if (sender is Button btn) btn.IsEnabled = false;

        if (_product == null || _userAuthenticationService.GetLoggedUserType() == UserType.NotLoggedIn) return;

        var popup = new AddToCartPopUp(_product);
        var result = await this.ShowPopupAsync(popup);

        if (result is not (bool and true)) return;
        var quantity = popup.ProductQuantity;
        _shopCartService.AddToCart(
            _product,
            _userAuthenticationService.GetLoggedUserId(),
            _userAuthenticationService.GetLoggedUsername(),
            quantity
        );

        await Shell.Current.GoToAsync("..");
    }
}