using Package_System_CRUD.BusinessLogic;
using Package_System_CRUD.BusinessLogic.Config;
using Package_System_CRUD.BusinessLogic.Interface;
using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Services;

namespace Package_System_CRUD.UserPages;

[QueryProperty("Username", "username")]
public partial class CustomerPage : ContentPage
{
    private readonly IModelService<Customer> _customerService;
    private readonly IModelService<Product> _productService;
    private readonly IModelService<Order> _orderService;
    private readonly IModelService<Manufacturer> _manufacturerService;
    private readonly ConfigurationProperties _properties;
    private readonly OrderCollectionViewModelRepository _orderCollectionViewModelRepository;
    public string? Username { get; set; }
    private int _pageNumber = 0;

    public CustomerPage(
        IModelService<Customer> customerService,
        IModelService<Product> productService,
        IModelService<Order> orderService,
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

        WelcomeLbl.Text = $"Welcome {Username}!";
        InitCustomerPage();
    }

    private void InitCustomerPage()
    {
        // var orderPageList = _orderService.GetPageList(_pageNumber, _properties.CustomerPageItemCount);
        // foreach (var order in orderPageList)
        // {
        //     TableSection.Insert(0, new ViewCell()
        //     {
        //         View = UserPageUtils.RenderOrderCellViews(order)
        //     });
        // }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        RenderCollectionViewItems();
    }

    private void OnPreviousBtnClicked(object? sender, EventArgs e)
    {
        if (_pageNumber > 0) _pageNumber--;
        PageNumLbl.Text = $"Page {_pageNumber}";
    }

    private void OnNextPageBtnClicked(object? sender, EventArgs e)
    {
        if (_orderService.GetCount() > _properties.CustomerPageItemCount) _pageNumber++;
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

    private void OnRefreshButtonClicked(object? sender, EventArgs e)
    {
        RenderCollectionViewItems();
    }

    private void RenderCollectionViewItems()
    {
        foreach (var order in _orderService.GetPageList(_pageNumber, _properties.CustomerPageItemCount))
        {
            _orderCollectionViewModelRepository.Add(
                order,
                _manufacturerService.FindById(order.ManufacturerId)?.Name,
                _productService.FindById(order.ProductId)?.Name
            );
        }

        collectionView.ItemsSource = _orderCollectionViewModelRepository.OrderCollection;
    }
}