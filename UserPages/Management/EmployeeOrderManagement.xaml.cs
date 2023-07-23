using Package_System_CRUD.BusinessLogic.Interface;
using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Services;

namespace Package_System_CRUD.UserPages.Management;

[QueryProperty(nameof(OrderCollectionViewModel), "OrderCollectionViewModel")]
public partial class EmployeeOrderManagement : ContentPage
{
    private OrderCollectionViewModel? _orderCollectionViewModel;
    private readonly IModelServiceExtended<Order> _orderService;

    public OrderCollectionViewModel? OrderCollectionViewModel
    {
        get => _orderCollectionViewModel;
        set
        {
            _orderCollectionViewModel = value;
            OnPropertyChanged();
        }
    }

    public EmployeeOrderManagement(IModelServiceExtended<Order> orderService)
    {
        InitializeComponent();
        BindingContext = this;
        _orderService = orderService;
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
        var order = _orderService.FindById(OrderCollectionViewModel.Id);
        if (order.Status == OrderStatus.Pending)
        {
            order.Status = OrderStatus.Received;
            order.SubmittedToManufacturer = DateTime.Today;
            _orderService.UpdateEntity(order);
            await Shell.Current.GoToAsync("..");
        }
    }

    private async void OnSendToCustomerButtonClicked(object? sender, EventArgs e)
    {
        var order = _orderService.FindById(OrderCollectionViewModel.Id);
        if (order.Status == OrderStatus.Sent)
        {
            order.Status = OrderStatus.ReadyToPickUp;
            order.SentToCustomer = DateTime.Today;
            _orderService.UpdateEntity(order);
            await Shell.Current.GoToAsync("..");
        }
    }
}