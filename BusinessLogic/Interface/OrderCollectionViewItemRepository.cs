using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.BusinessLogic.Interface
{
    public class OrderCollectionViewItemRepository
    {
        public List<OrderCollectionViewItem> OrderCollection { get; set; } = new();

        public void Add(Order order, string manufacturer, string product)
        {
            OrderCollection.Add(new OrderCollectionViewItem
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                ManufacturerName = manufacturer,
                ProductName = product,
                Quantity = order.Quantity,
                Status = order.Status,
                SubmittedToEmployee = order.SubmittedToEmployee,
                SubmittedToManufacturer = order.SubmittedToManufacturer,
                OrderRealized = order.OrderRealized,
                SentToCustomer = order.SentToCustomer,
                Completed = order.Completed
            });
        }
    }
}