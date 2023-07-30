using Package_System_CRUD.BusinessLogic;
using Package_System_CRUD.BusinessLogic.Config;
using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Services;

namespace Package_System_CRUD.UserPages.Shop;

public partial class ShopPage : ContentPage
{
    private readonly ShopCartService _shopCartService;
    private readonly IProductService<Product> _productService;
    private readonly UserAuthenticationService _userAuthenticationService;
    private readonly ConfigurationProperties _properties;
    private int _pageNumber = 0;
    private int _itemCountOnPage = 0;

    private Button? _addProductButton;
    private Button? _shoppingCartButton;


    public ShopPage(
        ShopCartService shopCartService,
        IProductService<Product> productService,
        ConfigurationProperties properties,
        UserAuthenticationService userAuthenticationService
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
        collectionView.ItemsSource = null;
        _itemCountOnPage = 0;

        List<Product> productList;
        if (_userAuthenticationService.UserType == UserType.Manufacturer)
        {
            productList = _productService
                .GetFilteredByManufacturerId(
                    _userAuthenticationService.LoggedUserId,
                    _pageNumber,
                    _properties.ShopPageItemCount
                );


            if (_addProductButton is not null) UpperControlsStackLayout.Remove(_addProductButton);

            Application.Current?.Dispatcher.Dispatch(() =>
            {
                var btn = new Button
                {
                    Text = "Add Product",
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    WidthRequest = 150
                };

                btn.Clicked += (sender, e) =>
                {
                    // todo
                };

                UpperControlsStackLayout.Add(btn);
                _addProductButton = btn;
            });
        }
        else
        {
            productList = _productService
                .GetPageList(_pageNumber, _properties.ShopPageItemCount);

            if (_userAuthenticationService.UserType == UserType.Customer)
            {
                if (_shoppingCartButton is not null) UpperControlsStackLayout.Remove(_shoppingCartButton);

                Application.Current?.Dispatcher.Dispatch(() =>
                {
                    var btn = new Button
                    {
                        Text = "Shopping Cart " + _shopCartService.Count,
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
        collectionView.ItemsSource = productList;
    }

    private void OnRefreshButtonClicked(object? sender, EventArgs e)
    {
        UpperControlsStackLayout.Remove(_addProductButton);
        UpperControlsStackLayout.Remove(_shoppingCartButton);

        RenderCollectionViewItems();
    }
}