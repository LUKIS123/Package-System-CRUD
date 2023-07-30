using CommunityToolkit.Maui.Views;
using Package_System_CRUD.BusinessLogic;
using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.UserPages.PopUps;

namespace Package_System_CRUD.UserPages.Shop;

[QueryProperty(nameof(Product), "Product")]
public partial class ProductSelectionPage : ContentPage
{
    private Product? _product;
    private readonly ShopCartService _shopCartService;
    private readonly UserAuthenticationService _userAuthenticationService;

    public Product? Product
    {
        get => _product;
        set
        {
            _product = value;
            OnPropertyChanged();
        }
    }

    public ProductSelectionPage(ShopCartService shopCartService, UserAuthenticationService userAuthenticationService)
    {
        InitializeComponent();
        BindingContext = this;
        _shopCartService = shopCartService;
        _userAuthenticationService = userAuthenticationService;
        _userAuthenticationService = userAuthenticationService;
    }

    private async void OnReturnButtonClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    private async void BuyProductButtonClicked(object? sender, EventArgs e)
    {
        if (_product == null || _userAuthenticationService.UserType == UserType.NotLoggedIn) return;

        var popup = new AddToCartPopUp(_product);
        var result = await this.ShowPopupAsync(popup);

        if (result is not (bool and true)) return;
        var quantity = popup.ProductQuantity;
        _shopCartService.AddToCart(
            _product,
            _userAuthenticationService.LoggedUserId,
            _userAuthenticationService.LoggedUser,
            quantity
        );

        await Shell.Current.GoToAsync("..");
    }
}