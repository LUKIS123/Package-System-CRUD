using Package_System_CRUD.BusinessLogic.Interface;
using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Services;

namespace Package_System_CRUD.UserPages.Management;

[QueryProperty(nameof(OrderCollectionViewModel), "OrderCollectionViewModel")]
public partial class ManufacturerOrderManagement : ContentPage
{
    private OrderCollectionViewItem? _orderCollectionViewModel;
    private readonly IOrderService<Order> _orderService;

    public OrderCollectionViewItem? OrderCollectionViewModel
    {
        get => _orderCollectionViewModel;
        set
        {
            _orderCollectionViewModel = value;
            OnPropertyChanged();
        }
    }

    public ManufacturerOrderManagement(IOrderService<Order> orderService)
    {
        InitializeComponent();
        BindingContext = this;
        _orderService = orderService;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        string note;
        switch (OrderCollectionViewModel?.Status)
        {
            case OrderStatus.Received:
                note = "NOTE: Order Status can be set to 'In Realization'";
                StatusChangedBtn.Text = "Set status to 'In Realization'";
                break;
            case OrderStatus.InRealization:
                note = "NOTE: Order Status can be set to 'Sent'";
                StatusChangedBtn.Text = "Set status to 'Sent'";
                break;
            default:
                StatusChangedBtn.Text = "No actions possible";
                StatusChangedBtn.TextColor = Colors.Grey;
                note = "NOTE: No actions possible...";

                break;
        }

        InfoTextCell.Text = note;
    }

    private async void OnReturnButtonClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    private async void OnStatusChangedButtonClicked(object? sender, EventArgs e)
    {
        if (OrderCollectionViewModel.Status is not (OrderStatus.Received or OrderStatus.InRealization))
        {
            return;
        }

        var order = _orderService.FindById(OrderCollectionViewModel.Id);
        if (OrderCollectionViewModel.Status == OrderStatus.Received)
        {
            order.Status = OrderStatus.InRealization;
            _orderService.UpdateEntity(order);

            StatusChangedBtn.Text = "Set status to 'Sent'";
            InfoTextCell.Text = "NOTE: Order Status can be set to 'Sent'";
        }

        if (OrderCollectionViewModel.Status == OrderStatus.InRealization)
        {
            order.Status = OrderStatus.Sent;
            order.OrderRealized = DateTime.Now;
            _orderService.UpdateEntity(order);

            StatusChangedBtn.Text = "No actions possible";
            StatusChangedBtn.TextColor = Colors.Grey;
            InfoTextCell.Text = "NOTE: No actions possible...";

            await Shell.Current.GoToAsync("..");
        }
    }
}