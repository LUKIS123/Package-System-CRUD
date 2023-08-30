using Package_System_CRUD.BusinessLogic.Config;
using Package_System_CRUD.BusinessLogic.Interface;
using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Services.Authentication;
using Package_System_CRUD.BusinessLogic.Services.Database.Orders;
using Package_System_CRUD.BusinessLogic.Services.Database.Products;
using Package_System_CRUD.UserPages.Management;

namespace Package_System_CRUD.UserPages;

public partial class ManufacturerPage : ContentPage
{
    private readonly IOrderService<Order> _orderService;
    private readonly IProductService<Product> _productService;
    private readonly IUserAuthenticationService _userAuthenticationService;
    private readonly ConfigurationProperties _properties;
    private readonly OrderCollectionViewItemRepository _orderCollectionViewItemRepository;

    public string Username { get; private set; } = string.Empty;
    public int ManufacturerId { get; private set; }
    private int _pageNumber = 0;
    private int _itemCountOnPage = 0;

    public ManufacturerPage(
        IOrderService<Order> orderService,
        IProductService<Product> productService,
        IUserAuthenticationService userAuthenticationService,
        ConfigurationProperties properties
    )
    {
        InitializeComponent();
        _orderService = orderService;
        _productService = productService;
        _properties = properties;
        _userAuthenticationService = userAuthenticationService;
        _orderCollectionViewItemRepository = new OrderCollectionViewItemRepository();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        Username = _userAuthenticationService.GetLoggedUsername();
        ManufacturerId = _userAuthenticationService.GetLoggedUserId();
        RenderCollectionViewItems();

        WelcomeLbl.Text = $"Welcome {Username}! : ID={ManufacturerId}";
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
        if (_itemCountOnPage < _properties.ManufacturerPageItemCount) return;
        _pageNumber++;
        RenderCollectionViewItems();

        PageNumLbl.Text = $"Page {_pageNumber}";
    }

    private async void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var selectedOrderViewItem = e.CurrentSelection.FirstOrDefault() as OrderCollectionViewItem;
        if (selectedOrderViewItem == null) return;

        var navigationParameter = new Dictionary<string, object>
        {
            { nameof(OrderCollectionViewItem), selectedOrderViewItem }
        };

        await Shell.Current.GoToAsync(nameof(ManufacturerOrderManagement), navigationParameter);
    }

    private void RenderCollectionViewItems()
    {
        OrderCollectionView.ItemsSource = null;
        _orderCollectionViewItemRepository.OrderCollection.Clear();

        _itemCountOnPage = 0;
        var ordersById = _orderService
            .GetFilteredByManufacturerId(
                ManufacturerId,
                _pageNumber,
                _properties.ManufacturerPageItemCount
            );

        foreach (var order in ordersById)
        {
            _orderCollectionViewItemRepository.Add(
                order,
                Username,
                _productService.FindById(order.ProductId)?.Name
            );
            _itemCountOnPage++;
        }

        OrderCollectionView.ItemsSource = _orderCollectionViewItemRepository.OrderCollection;
    }

    private void OnRefreshButtonClicked(object? sender, EventArgs e)
    {
        RenderCollectionViewItems();
    }
}