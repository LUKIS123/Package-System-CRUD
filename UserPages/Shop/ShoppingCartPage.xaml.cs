using Package_System_CRUD.BusinessLogic;
using Package_System_CRUD.BusinessLogic.Config;
using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.UserPages.Shop;

public partial class ShoppingCartPage : ContentPage
{
    private readonly ShopCartService _shopCartService;
    private readonly ConfigurationProperties _properties;
    private int _pageNumber = 0;
    private int _itemCountOnPage = 0;

    public ShoppingCartPage(ShopCartService shopCartService, ConfigurationProperties properties)
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
        collectionView.ItemsSource = _shopCartService.Orders.Values.ToList();
        _itemCountOnPage = _shopCartService.Count;
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

    private void OnSubmitOrdersButtonClicked(object? sender, EventArgs e)
    {
        _shopCartService.SubmitOrders();
        RenderCollectionViewItems();
    }
}