using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.BusinessLogic.Interface
{
    public class OrderCollectionViewModelRepository
    {
        public List<OrderCollectionViewModel> OrderCollection { get; set; } = new();

        public void Add(Order order, string manufacturer, string product)
        {
            OrderCollection.Add(new OrderCollectionViewModel
            {
                CustomerName = order.CustomerName,
                ManufacturerName = manufacturer,
                ProductName = product,
                Quantity = order.Quantity,
                Status = order.Status,
                SubmittedToEmployee = order.SubmittedToEmployee,
                SubmittedToManufacturer = order.SubmittedToManufacturer,
                OrderRealized = order.OrderRealized,
                SentToCustomer = order.OrderRealized,
                Completed = order.Completed
            });
        }
    }
}