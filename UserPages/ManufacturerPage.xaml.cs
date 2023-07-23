using Package_System_CRUD.BusinessLogic;
using Package_System_CRUD.BusinessLogic.Config;
using Package_System_CRUD.BusinessLogic.Interface;
using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Services;
using Package_System_CRUD.UserPages.Management;

namespace Package_System_CRUD.UserPages;

public partial class ManufacturerPage : ContentPage
{
    private readonly IModelServiceExtended<Order> _orderService;
    private readonly IModelService<Product> _productService;
    private readonly ConfigurationProperties _properties;
    private readonly OrderCollectionViewModelRepository _orderCollectionViewModelRepository;
    private readonly UserAuthenticationService _userAuthenticationService;
    public string Username { get; private set; } = string.Empty;
    public int ManufacturerId { get; private set; }
    private int _pageNumber = 0;
    private int _itemCountOnPage = 0;

    public ManufacturerPage(
        IModelServiceExtended<Order> orderService,
        IModelService<Product> productService,
        ConfigurationProperties properties,
        UserAuthenticationService userAuthenticationService
    )
    {
        InitializeComponent();
        _orderService = orderService;
        _productService = productService;
        _properties = properties;
        _userAuthenticationService = userAuthenticationService;
        _orderCollectionViewModelRepository = new OrderCollectionViewModelRepository();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        Username = _userAuthenticationService.LoggedUser;
        ManufacturerId = _userAuthenticationService.LoggedUserId;
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
        var selectedOrderViewModel = e.CurrentSelection.FirstOrDefault() as OrderCollectionViewModel;
        if (selectedOrderViewModel == null) return;

        var navigationParameter = new Dictionary<string, object>
        {
            { nameof(OrderCollectionViewModel), selectedOrderViewModel }
        };

        await Shell.Current.GoToAsync(nameof(ManufacturerOrderManagement), navigationParameter);
    }

    private void RenderCollectionViewItems()
    {
        collectionView.ItemsSource = null;
        _orderCollectionViewModelRepository.OrderCollection.Clear();

        _itemCountOnPage = 0;
        var ordersById = _orderService
            .GetFilteredByManufacturerId(
                ManufacturerId,
                _pageNumber,
                _properties.ManufacturerPageItemCount
            );

        foreach (var order in ordersById)
        {
            _orderCollectionViewModelRepository.Add(
                order,
                Username,
                _productService.FindById(order.ProductId)?.Name
            );
            _itemCountOnPage++;
        }

        collectionView.ItemsSource = _orderCollectionViewModelRepository.OrderCollection;
    }

    private void OnRefreshButtonClicked(object? sender, EventArgs e)
    {
        RenderCollectionViewItems();
    }
}