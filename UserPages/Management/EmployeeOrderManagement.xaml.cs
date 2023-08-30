using Package_System_CRUD.BusinessLogic.DateTimeProvider;
using Package_System_CRUD.BusinessLogic.Interface;
using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Services.Database.Orders;

namespace Package_System_CRUD.UserPages.Management;

[QueryProperty(nameof(OrderCollectionViewModel), "OrderCollectionViewModel")]
public partial class EmployeeOrderManagement : ContentPage
{
    private OrderCollectionViewItem? _orderCollectionViewModel;
    private readonly IOrderService<Order> _orderService;
    private readonly IDateTimeProvider _dateTimeProvider;

    public OrderCollectionViewItem? OrderCollectionViewModel
    {
        get => _orderCollectionViewModel;
        set
        {
            _orderCollectionViewModel = value;
            OnPropertyChanged();
        }
    }

    public EmployeeOrderManagement(IOrderService<Order> orderService, IDateTimeProvider dateTimeProvider)
    {
        InitializeComponent();
        BindingContext = this;
        _orderService = orderService;
        _dateTimeProvider = dateTimeProvider;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        var note = OrderCollectionViewModel?.Status switch
        {
            OrderStatus.Pending => "NOTE: Order pending to be sent to Manufacturer",
            OrderStatus.Sent => "NOTE: Order ready to be sent to Customer",
            _ => "NOTE: No actions possible..."
        };
        InfoTextCell.Text = note;
    }

    private async void OnReturnButtonClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    private async void OnForwardToManufacturerButtonClicked(object? sender, EventArgs e)
    {
        if (OrderCollectionViewModel.Status != OrderStatus.Pending) return;

        var order = _orderService.FindById(OrderCollectionViewModel.Id);
        if (order == null) return;

        order.Status = OrderStatus.Received;
        order.SubmittedToManufacturer = _dateTimeProvider.GetDateTime();
        _orderService.UpdateEntity(order);
        await Shell.Current.GoToAsync("..");
    }

    private async void OnSendToCustomerButtonClicked(object? sender, EventArgs e)
    {
        if (OrderCollectionViewModel.Status != OrderStatus.Sent) return;

        var order = _orderService.FindById(OrderCollectionViewModel.Id);
        if (order == null) return;

        order.Status = OrderStatus.ReadyToPickUp;
        order.SentToCustomer = _dateTimeProvider.GetDateTime();
        _orderService.UpdateEntity(order);
        await Shell.Current.GoToAsync("..");
    }
}