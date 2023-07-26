using Package_System_CRUD.BusinessLogic;
using Package_System_CRUD.BusinessLogic.Config;
using Package_System_CRUD.BusinessLogic.Interface;
using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Services;

namespace Package_System_CRUD.UserPages.Shop;

public partial class ShopPage : ContentPage
{
    // moze dodac rozne implementacje shop page dla roznych uzytkownikow-> customer i employee widza wszystko a shop widzi swoje i moze doddac nowe

    private readonly IModelServiceExtended<Order> _orderService;
    private readonly IModelService<Product> _productService;
    private readonly IModelService<Manufacturer> _manufacturerService;
    private readonly UserAuthenticationService _userAuthenticationService;
    private readonly ConfigurationProperties _properties;
    private readonly OrderCollectionViewModelRepository _orderCollectionViewModelRepository;
    public string Username { get; private set; } = string.Empty;
    public int UserId { get; private set; }
    public UserType UserType { get; private set; }
    private int _pageNumber = 0;
    private int _itemCountOnPage = 0;

    public ShopPage(
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
        _userAuthenticationService = userAuthenticationService;
        _orderCollectionViewModelRepository = new OrderCollectionViewModelRepository();
    }
}