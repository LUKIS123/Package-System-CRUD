using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Services;

namespace Package_System_CRUD.BusinessLogic
{
    public class ShopCartService
    {
        private readonly IOrderService<Order> _orderService;
        private readonly Dictionary<int, Order> _orderDictionary = new();

        public ShopCartService(IOrderService<Order> orderService)
        {
            _orderService = orderService;
        }

        public Dictionary<int, Order> Orders => _orderDictionary;

        public int Count => _orderDictionary.Count;

        public void AddToCart(Product product, int userId, string username, int itemCount)
        {
            if (_orderDictionary.ContainsKey(product.Id))
            {
                _orderDictionary[product.Id].Quantity += itemCount;
                return;
            }

            _orderDictionary.Add(
                product.Id,
                new Order
                {
                    CustomerId = userId,
                    ManufacturerId = product.ManufacturerId,
                    ProductId = product.Id,
                    Quantity = itemCount,
                    CustomerName = username,
                    Status = OrderStatus.InCart,
                    SubmittedToEmployee = null,
                    SubmittedToManufacturer = null,
                    OrderRealized = null,
                    SentToCustomer = null,
                    Completed = null
                }
            );
        }

        public void RemoveFromCart(int productId)
        {
            _orderDictionary.Remove(productId);
        }

        public void SubmitOrders()
        {
            if (_orderDictionary.Count == 0) return;
            foreach (var keyValuePair in _orderDictionary)
            {
                keyValuePair.Value.Status = OrderStatus.Pending;
                keyValuePair.Value.SubmittedToEmployee = DateTime.Now;
                _orderService.AddToDatabase(keyValuePair.Value);
            }

            _orderDictionary.Clear();
        }
    }
}