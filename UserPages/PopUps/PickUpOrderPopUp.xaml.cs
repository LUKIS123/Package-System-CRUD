using CommunityToolkit.Maui.Views;
using Package_System_CRUD.BusinessLogic.Interface;
using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.UserPages.PopUps;

[QueryProperty(nameof(OrderCollectionViewModel), "OrderCollectionViewModel")]
public partial class PickUpOrderPopUp : Popup
{
    private OrderCollectionViewModel? _orderCollectionViewModel;
    private bool _isPickedUp = false;

    public OrderCollectionViewModel? OrderCollectionViewModel
    {
        get => _orderCollectionViewModel;
        set
        {
            _orderCollectionViewModel = value;
            OnPropertyChanged();
        }
    }

    public PickUpOrderPopUp(OrderCollectionViewModel? orderCollectionViewModel)
    {
        InitializeComponent();
        OrderCollectionViewModel = orderCollectionViewModel;

        OverviewLbl.Text = OrderCollectionViewModel?.Overview;
        InfoLbl.Text = OrderCollectionViewModel?.Status == OrderStatus.ReadyToPickUp
            ? "Order is ready to Pick Up!"
            : "Order is yet to be realized!";
    }

    private void OnPickUpButtonClicked(object? sender, EventArgs e)
    {
        if (OrderCollectionViewModel?.Status == OrderStatus.ReadyToPickUp)
        {
            OrderCollectionViewModel.Status = OrderStatus.PickedUp;
            _isPickedUp = true;
            InfoLbl.Text = "Order successfully Picked Up!";
            InfoLbl.TextColor = Colors.LightGreen;
        }
        else
        {
            InfoLbl.Text = "Cannot pick up order!";
            InfoLbl.TextColor = Colors.Red;
        }
    }

    private void OnCloseButtonClicked(object? sender, EventArgs e) => Close(_isPickedUp);
}