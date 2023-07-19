using CommunityToolkit.Maui.Views;
using Package_System_CRUD.BusinessLogic;
using Package_System_CRUD.BusinessLogic.Config;
using Package_System_CRUD.BusinessLogic.Interface;
using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Services;
using Package_System_CRUD.UserPages.PopUps;

namespace Package_System_CRUD.UserPages;

[QueryProperty("Username", "username")]
public partial class CustomerPage : ContentPage
{
    private readonly IModelServiceExtended<Order> _orderService;
    private readonly IModelService<Product> _productService;
    private readonly IModelService<Manufacturer> _manufacturerService;
    private readonly ConfigurationProperties _properties;
    private readonly OrderCollectionViewModelRepository _orderCollectionViewModelRepository;
    public string Username { get; init; }
    public int UserId { get; init; }
    private int _pageNumber = 0;
    private int _itemCountOnPage = 0;

    public CustomerPage(
        IModelServiceExtended<Order> orderService,
        IModelService<Product> productService,
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
        _orderCollectionViewModelRepository = new OrderCollectionViewModelRepository();

        Username = userAuthenticationService.LoggedUser;
        UserId = userAuthenticationService.LoggedUserId;

        WelcomeLbl.Text = $"Welcome {Username}! : ID={UserId}";
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
        if (_itemCountOnPage < _properties.CustomerPageItemCount) return;
        _pageNumber++;
        _orderCollectionViewModelRepository.OrderCollection.Clear();
        collectionView.ItemsSource = null;
        RenderCollectionViewItems();
        PageNumLbl.Text = $"Page {_pageNumber}";
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedOrderViewModel = e.CurrentSelection.FirstOrDefault() as OrderCollectionViewModel;
        if (selectedOrderViewModel == null) return;

        var popup = new PickUpOrderPopUp(selectedOrderViewModel);
        var result = await this.ShowPopupAsync(popup);
        if (result is not (bool and true)) return;

        var order = _orderService.FindById(selectedOrderViewModel.Id);
        if (order is null) return;

        order.Status = selectedOrderViewModel.Status;
        _orderService.UpdateEntity(order);
    }

    private void RenderCollectionViewItems()
    {
        _itemCountOnPage = 0;
        var ordersById = _orderService
            .GetFilteredByUserId(
                UserId,
                _pageNumber,
                _properties.CustomerPageItemCount
            );

        foreach (var order in ordersById)
        {
            _orderCollectionViewModelRepository.Add(
                order,
                _manufacturerService.FindById(order.ManufacturerId)?.Name,
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