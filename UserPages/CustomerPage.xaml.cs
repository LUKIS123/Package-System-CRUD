using Package_System_CRUD.BusinessLogic;
using Package_System_CRUD.BusinessLogic.Config;
using Package_System_CRUD.BusinessLogic.Interface;
using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Services;

namespace Package_System_CRUD.UserPages;

[QueryProperty("Username", "username")]
public partial class CustomerPage : ContentPage
{
    private readonly IModelServiceExtended<Order> _orderService;
    private readonly IModelService<Customer> _customerService;
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
        IModelService<Customer> customerService,
        IModelService<Product> productService,
        IModelService<Manufacturer> manufacturerService,
        ConfigurationProperties properties,
        UserAuthenticationService userAuthenticationService
    )
    {
        InitializeComponent();
        _customerService = customerService;
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
        // Debug.WriteLine("---> Item changed clicked!");
        //
        // var navigationParameter = new Dictionary<string, object>
        // {
        //     { nameof(Order), e.CurrentSelection.FirstOrDefault() as Order }
        // };
        //
        // await Shell.Current.GoToAsync(nameof(ManageToDoPage), navigationParameter);
    }

    private void RenderCollectionViewItems()
    {
        _itemCountOnPage = 0;
        var ordersById = _orderService.GetFilteredByUserId(UserId, _pageNumber, _properties.CustomerPageItemCount);

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