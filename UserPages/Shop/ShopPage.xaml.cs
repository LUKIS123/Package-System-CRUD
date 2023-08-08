using Package_System_CRUD.BusinessLogic.Config;
using Package_System_CRUD.BusinessLogic.Enums;
using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Services.Authentication;
using Package_System_CRUD.BusinessLogic.Services.Database.Products;
using Package_System_CRUD.BusinessLogic.Services.ShoppingCart;

namespace Package_System_CRUD.UserPages.Shop;

public partial class ShopPage : ContentPage
{
    private readonly IShopCartService _shopCartService;
    private readonly IProductService<Product> _productService;
    private readonly IUserAuthenticationService _userAuthenticationService;
    private readonly ConfigurationProperties _properties;
    private int _pageNumber = 0;
    private int _itemCountOnPage = 0;

    private Button? _addProductButton;
    private Button? _shoppingCartButton;

    public ShopPage(
        IShopCartService shopCartService,
        IProductService<Product> productService,
        IUserAuthenticationService userAuthenticationService,
        ConfigurationProperties properties
    )
    {
        InitializeComponent();
        _productService = productService;
        _shopCartService = shopCartService;
        _properties = properties;
        _userAuthenticationService = userAuthenticationService;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        RenderCollectionViewItems();
    }

    private async void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var selectedProduct = e.CurrentSelection.FirstOrDefault() as Product;
        if (selectedProduct == null) return;

        var navigationParameter = new Dictionary<string, object>
        {
            { nameof(Product), selectedProduct }
        };

        await Shell.Current.GoToAsync(nameof(ProductSelectionPage), navigationParameter);
    }

    private void OnNextPageBtnClicked(object? sender, EventArgs e)
    {
        if (_itemCountOnPage < _properties.CustomerPageItemCount) return;
        _pageNumber++;
        RenderCollectionViewItems();

        PageNumLbl.Text = $"Page {_pageNumber}";
    }

    private void OnPreviousBtnClicked(object? sender, EventArgs e)
    {
        if (_pageNumber <= 0) return;
        _pageNumber--;
        RenderCollectionViewItems();

        PageNumLbl.Text = $"Page {_pageNumber}";
    }

    private void RenderCollectionViewItems()
    {
        ProductCollectionView.ItemsSource = null;
        if (_shoppingCartButton is not null) UpperControlsStackLayout.Remove(_shoppingCartButton);
        if (_addProductButton is not null) UpperControlsStackLayout.Remove(_addProductButton);
        _itemCountOnPage = 0;

        List<Product> productList;
        if (_userAuthenticationService.GetLoggedUserType() == UserType.Manufacturer)
        {
            productList = _productService
                .GetFilteredByManufacturerId(
                    _userAuthenticationService.GetLoggedUserId(),
                    _pageNumber,
                    _properties.ShopPageItemCount
                );

            Application.Current?.Dispatcher.Dispatch(() =>
            {
                var btn = new Button
                {
                    Text = "Add Product",
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    WidthRequest = 150
                };

                btn.Clicked += (sender, e) => { Shell.Current.GoToAsync(nameof(AddNewProductPage)); };

                UpperControlsStackLayout.Add(btn);
                _addProductButton = btn;
            });
        }
        else
        {
            productList = _productService
                .GetPageList(_pageNumber, _properties.ShopPageItemCount);

            if (_userAuthenticationService.GetLoggedUserType() == UserType.Customer)
            {
                Application.Current?.Dispatcher.Dispatch(() =>
                {
                    var btn = new Button
                    {
                        Text = "Shopping Cart " + _shopCartService.GetProductCount(),
                        HorizontalOptions = LayoutOptions.Center,
                        VerticalOptions = LayoutOptions.Center,
                        WidthRequest = 150
                    };

                    btn.Clicked += (sender, args) => { Shell.Current.GoToAsync(nameof(ShoppingCartPage)); };

                    UpperControlsStackLayout.Add(btn);
                    _shoppingCartButton = btn;
                });
            }
        }

        _itemCountOnPage = productList.Count;
        ProductCollectionView.ItemsSource = productList;
    }

    private void OnRefreshButtonClicked(object? sender, EventArgs e)
    {
        UpperControlsStackLayout.Remove(_addProductButton);
        UpperControlsStackLayout.Remove(_shoppingCartButton);
        RenderCollectionViewItems();
    }
}