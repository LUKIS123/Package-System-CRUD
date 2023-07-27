using Package_System_CRUD.BusinessLogic;
using Package_System_CRUD.BusinessLogic.Config;
using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Services;

namespace Package_System_CRUD.UserPages.Shop;

public partial class ShopPage : ContentPage
{
    private readonly IOrderService<Order> _orderService;
    private readonly IProductService<Product> _productService;
    private readonly IModelService<Manufacturer> _manufacturerService;
    private readonly UserAuthenticationService _userAuthenticationService;
    private readonly ConfigurationProperties _properties;
    private int _pageNumber = 0;
    private int _itemCountOnPage = 0;

    public ShopPage(
        IOrderService<Order> orderService,
        IProductService<Product> productService,
        IModelService<Manufacturer> manufacturerService,
        ConfigurationProperties properties,
        UserAuthenticationService userAuthenticationService
    )
    {
        InitializeComponent();
        _productService = productService;
        _orderService = orderService;
        _properties = properties;
        _manufacturerService = manufacturerService;
        _userAuthenticationService = userAuthenticationService;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        RenderCollectionViewItems();
    }

    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        // todo
        // dodac moze nieuzywana properte jesli chodzi o Order i zzrezygnowac z OrderCollectionViewModel
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
        }
        else
        {
            productList = _productService
                .GetPageList(_pageNumber, _properties.ShopPageItemCount);
        }

        _itemCountOnPage = productList.Count;
        collectionView.ItemsSource = productList;
    }

    private void OnRefreshButtonClicked(object? sender, EventArgs e)
    {
        RenderCollectionViewItems();
    }
}