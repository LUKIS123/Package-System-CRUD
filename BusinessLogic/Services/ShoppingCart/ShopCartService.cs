using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Services.Database.Orders;

namespace Package_System_CRUD.BusinessLogic.Services.ShoppingCart
{
    public class ShopCartService : IShopCartService
    {
        private readonly IOrderService<Order> _orderService;
        private readonly Dictionary<int, Order> _orderDictionary = new();

        public ShopCartService(IOrderService<Order> orderService)
        {
            _orderService = orderService;
        }

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

        public Dictionary<int, Order> GetOrderDictionary()
        {
            return _orderDictionary;
        }

        public int GetProductCount()
        {
            return _orderDictionary.Count;
        }
    }
}