using Package_System_CRUD.BusinessLogic.Config;
using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Services.ShoppingCart;

namespace Package_System_CRUD.UserPages.Shop;

public partial class ShoppingCartPage : ContentPage
{
    private readonly IShopCartService _shopCartService;
    private readonly ConfigurationProperties _properties;
    private int _pageNumber = 0;
    private int _itemCountOnPage = 0;

    public ShoppingCartPage(
        IShopCartService shopCartService,
        ConfigurationProperties properties
    )
    {
        InitializeComponent();
        _shopCartService = shopCartService;
        _properties = properties;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        RenderCollectionViewItems();
    }

    private void RenderCollectionViewItems()
    {
        collectionView.ItemsSource = null;
        collectionView.ItemsSource = _shopCartService.GetOrderDictionary().Values.ToList();
        _itemCountOnPage = _shopCartService.GetProductCount();
    }

    private void OnPreviousBtnClicked(object? sender, EventArgs e)
    {
        if (_pageNumber <= 0) return;
        _pageNumber--;
        RenderCollectionViewItems();

        PageNumLbl.Text = $"Page {_pageNumber}";
    }

    private void OnNextPageBtnClicked(object? sender, EventArgs e)
    {
        if (_itemCountOnPage < _properties.ShopPageItemCount) return;
        _pageNumber++;
        RenderCollectionViewItems();

        PageNumLbl.Text = $"Page {_pageNumber}";
    }

    private void OnRemoveButtonClicked(object? sender, EventArgs e)
    {
        var selectedOrderViewItem = (sender as Button)?.BindingContext as Order;
        if (selectedOrderViewItem == null) return;
        _shopCartService.RemoveFromCart(selectedOrderViewItem.ProductId);
        RenderCollectionViewItems();
    }

    private async void OnSubmitOrdersButtonClicked(object? sender, EventArgs e)
    {
        if (sender is Button btn) btn.IsEnabled = false;
        _shopCartService.SubmitOrders();
        await Shell.Current.GoToAsync("..");
    }
}