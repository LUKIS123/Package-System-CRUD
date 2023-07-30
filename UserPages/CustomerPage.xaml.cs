using CommunityToolkit.Maui.Views;
using Package_System_CRUD.BusinessLogic;
using Package_System_CRUD.BusinessLogic.Config;
using Package_System_CRUD.BusinessLogic.Interface;
using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Services;
using Package_System_CRUD.UserPages.PopUps;

namespace Package_System_CRUD.UserPages;

public partial class CustomerPage : ContentPage
{
    private readonly IOrderService<Order> _orderService;
    private readonly IProductService<Product> _productService;
    private readonly IModelService<Manufacturer> _manufacturerService;
    private readonly ConfigurationProperties _properties;
    private readonly OrderCollectionViewItemRepository _orderCollectionViewItemRepository;
    private readonly UserAuthenticationService _userAuthenticationService;
    public string Username { get; private set; } = string.Empty;
    public int UserId { get; private set; }
    private int _pageNumber = 0;
    private int _itemCountOnPage = 0;

    public CustomerPage(
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
        _orderCollectionViewItemRepository = new OrderCollectionViewItemRepository();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        Username = _userAuthenticationService.LoggedUser;
        UserId = _userAuthenticationService.LoggedUserId;
        RenderCollectionViewItems();

        WelcomeLbl.Text = $"Welcome {Username}! : ID={UserId}";
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
        if (_itemCountOnPage < _properties.CustomerPageItemCount) return;
        _pageNumber++;
        RenderCollectionViewItems();

        PageNumLbl.Text = $"Page {_pageNumber}";
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var selectedOrderViewItem = e.CurrentSelection.FirstOrDefault() as OrderCollectionViewItem;
        if (selectedOrderViewItem == null) return;

        var popup = new PickUpOrderPopUp(selectedOrderViewItem);
        var result = await this.ShowPopupAsync(popup);
        if (result is not (bool and true)) return;

        var order = _orderService.FindById(selectedOrderViewItem.Id);
        if (order is null) return;

        order.Status = OrderStatus.PickedUp;
        order.SubmittedToEmployee = DateTime.Now;
        _orderService.UpdateEntity(order);
    }

    private void RenderCollectionViewItems()
    {
        collectionView.ItemsSource = null;
        _orderCollectionViewItemRepository.OrderCollection.Clear();

        _itemCountOnPage = 0;
        var ordersById = _orderService
            .GetFilteredByUserId(
                UserId,
                _pageNumber,
                _properties.CustomerPageItemCount
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