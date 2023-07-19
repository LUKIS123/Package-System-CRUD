using Package_System_CRUD.BusinessLogic;
using Package_System_CRUD.BusinessLogic.Config;
using Package_System_CRUD.BusinessLogic.Interface;
using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Services;

namespace Package_System_CRUD.UserPages;

public partial class ManufacturerPage : ContentPage
{
    private readonly IModelServiceExtended<Order> _orderService;
    private readonly IModelService<Product> _productService;
    private readonly ConfigurationProperties _properties;
    private readonly OrderCollectionViewModelRepository _orderCollectionViewModelRepository;
    public string Username { get; init; }
    public int ManufacturerId { get; init; }
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
        _orderCollectionViewModelRepository = new OrderCollectionViewModelRepository();

        Username = userAuthenticationService.LoggedUser;
        ManufacturerId = userAuthenticationService.LoggedUserId;

        WelcomeLbl.Text = $"Welcome {Username}! : ID={ManufacturerId}";
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        RenderCollectionViewItems();
    }


    private void OnPreviousBtnClicked(object? sender, EventArgs e)
    {
        if (_pageNumber <= 0) return;
        _pageNumber--;
        _orderCollectionViewModelRepository.OrderCollection.Clear();
        collectionView.ItemsSource = null;
        RenderCollectionViewItems();
        PageNumLbl.Text = $"Page {_pageNumber}";
    }

    private void OnNextPageBtnClicked(object? sender, EventArgs e)
    {
        if (_itemCountOnPage < _properties.ManufacturerPageItemCount) return;
        _pageNumber++;
        _orderCollectionViewModelRepository.OrderCollection.Clear();
        collectionView.ItemsSource = null;
        RenderCollectionViewItems();
        PageNumLbl.Text = $"Page {_pageNumber}";
    }

    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
    }

    private void RenderCollectionViewItems()
    {
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
        _orderCollectionViewModelRepository.OrderCollection.Clear();
        collectionView.ItemsSource = null;
        RenderCollectionViewItems();
    }
}