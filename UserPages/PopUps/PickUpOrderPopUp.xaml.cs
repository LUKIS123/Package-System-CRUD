using CommunityToolkit.Maui.Views;
using Package_System_CRUD.BusinessLogic.Interface;
using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.UserPages.PopUps;

[QueryProperty(nameof(BusinessLogic.Interface.OrderCollectionViewItem), "OrderCollectionViewItem")]
public partial class PickUpOrderPopUp : Popup
{
    private OrderCollectionViewItem? _orderCollectionViewItem;
    private bool _isPickedUp = false;

    public OrderCollectionViewItem? OrderCollectionViewItem
    {
        get => _orderCollectionViewItem;
        set
        {
            _orderCollectionViewItem = value;
            OnPropertyChanged();
        }
    }

    public PickUpOrderPopUp(OrderCollectionViewItem? orderCollectionViewItem)
    {
        InitializeComponent();
        OrderCollectionViewItem = orderCollectionViewItem;

        OverviewLbl.Text = OrderCollectionViewItem?.Details;
        var status = OrderCollectionViewItem?.Status;
        InfoLbl.Text = status switch
        {
            (OrderStatus.ReadyToPickUp) => "Order is ready to Pick Up!",
            (OrderStatus.PickedUp) => "Already picked up this order!",
            _ => "Order is yet to be realized!"
        };
    }

    private void OnPickUpButtonClicked(object? sender, EventArgs e)
    {
        if (OrderCollectionViewItem?.Status == OrderStatus.ReadyToPickUp)
        {
            OrderCollectionViewItem.Status = OrderStatus.PickedUp;
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