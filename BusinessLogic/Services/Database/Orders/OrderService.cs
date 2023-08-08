using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Repositories;

namespace Package_System_CRUD.BusinessLogic.Services.Database.Orders
{
    public class OrderService : IOrderService<Order>
    {
        private readonly IModelRepository<Order> _orderRepository;

        public OrderService(IModelRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> GetPageList(int pageNumber, int numberOfElements)
        {
            return _orderRepository
                .LoadPage(pageNumber, numberOfElements);
        }

        public Order? FindById(int id)
        {
            return _orderRepository
                .FindById(x => x.Id == id);
        }

        public Order? FindByName(string name)
        {
            return _orderRepository
                .FindByName(x => x.CustomerName == name);
        }

        public void AddToDatabase(Order model)
        {
            _orderRepository.SaveEntity(model);
        }

        public void RemoveFromDatabase(Order model)
        {
            _orderRepository.DeleteEntity(model);
        }

        public void UpdateEntity(Order entity)
        {
            _orderRepository.UpdateEntity(entity);
        }

        public List<Order> GetFilteredByUserId(int id)
        {
            return _orderRepository
                .GetFiltered(
                    (order) => order.CustomerId == id
                );
        }

        public List<Order> GetFilteredByManufacturerId(int id)
        {
            return _orderRepository
                .GetFiltered(
                    (order) => order.ManufacturerId == id
                );
        }

        public List<Order> GetFilteredByStatus(OrderStatus orderStatus)
        {
            return _orderRepository
                .GetFiltered(
                    (order) => order.Status == orderStatus
                );
        }

        public List<Order> GetFilteredByUserId(int id, int pageNumber, int numberOfElements)
        {
            return _orderRepository
                .GetFiltered(
                    (order) => order.CustomerId == id,
                    pageNumber,
                    numberOfElements
                );
        }

        public List<Order> GetFilteredByManufacturerId(int id, int pageNumber, int numberOfElements)
        {
            return _orderRepository
                .GetFiltered(
                    (order) => order.ManufacturerId == id,
                    pageNumber,
                    numberOfElements
                );
        }

        public List<Order> GetFilteredByStatus(OrderStatus orderStatus, int pageNumber, int numberOfElements)
        {
            return _orderRepository
                .GetFiltered(
                    (order) => order.Status == orderStatus,
                    pageNumber,
                    numberOfElements
                );
        }
    }
}