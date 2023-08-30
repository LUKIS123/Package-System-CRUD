using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.BusinessLogic.Services.ShoppingCart
{
    public interface IShopCartService
    {
        public void AddToCart(Product product, int userId, string username, int itemCount);
        public void RemoveFromCart(int productId);
        public void SubmitOrders();
        public Dictionary<int, Order> GetOrderDictionary();
        public int GetProductCount();
    }
}