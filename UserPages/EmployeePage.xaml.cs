using Package_System_CRUD.BusinessLogic.Config;
using Package_System_CRUD.BusinessLogic.Interface;
using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Services.Authentication;
using Package_System_CRUD.BusinessLogic.Services.Database;
using Package_System_CRUD.BusinessLogic.Services.Database.Orders;
using Package_System_CRUD.BusinessLogic.Services.Database.Products;
using Package_System_CRUD.UserPages.Management;

namespace Package_System_CRUD.UserPages;

public partial class EmployeePage : ContentPage
{
    private readonly IOrderService<Order> _orderService;
    private readonly IProductService<Product> _productService;
    private readonly IModelService<Manufacturer> _manufacturerService;
    private readonly IUserAuthenticationService _userAuthenticationService;
    private readonly ConfigurationProperties _properties;
    private readonly OrderCollectionViewItemRepository _orderCollectionViewItemRepository;
    private int _pageNumber = 0;
    private int _itemCountOnPage = 0;

    public EmployeePage(
        IOrderService<Order> orderService,
        IProductService<Product> productService,
        IModelService<Manufacturer> manufacturerService,
        IUserAuthenticationService userAuthenticationService,
        ConfigurationProperties properties
    )
    {
        InitializeComponent();
        _orderService = orderService;
        _productService = productService;
        _manufacturerService = manufacturerService;
        _userAuthenticationService = userAuthenticationService;
        _properties = properties;
        _orderCollectionViewItemRepository = new OrderCollectionViewItemRepository();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        RenderCollectionViewItems();

        WelcomeLbl.Text = $"Welcome {_userAuthenticationService.GetLoggedUsername()}!";
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
        if (_itemCountOnPage < _properties.EmployeePageItemCount) return;
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

        await Shell.Current.GoToAsync(nameof(EmployeeOrderManagement), navigationParameter);
    }

    private void RenderCollectionViewItems()
    {
        collectionView.ItemsSource = null;
        _orderCollectionViewItemRepository.OrderCollection.Clear();

        _itemCountOnPage = 0;
        var ordersById = _orderService
            .GetPageList(
                _pageNumber,
                _properties.EmployeePageItemCount
            );

        foreach (var order in ordersById)
        {
            _orderCollectionViewItemRepository.Add(
                order,
                _manufacturerService.FindById(order.ManufacturerId)?.Name,
                _productService.FindById(order.ProductId)?.Name
            );
            _itemCountOnPage++;
        }

        collectionView.ItemsSource = _orderCollectionViewItemRepository.OrderCollection;
    }

    private void OnRefreshButtonClicked(object? sender, EventArgs e)
    {
        RenderCollectionViewItems();
    }
}