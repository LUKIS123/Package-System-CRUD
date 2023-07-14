using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Repositories;

namespace Package_System_CRUD.BusinessLogic.Services
{
    public class OrderService : IModelService<Order>
    {
        private readonly IModelRepository<Order> _orderRepository;

        public OrderService(IModelRepository<Order> orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> GetPageList(int pageNumber, int numberOfElements)
        {
            return _orderRepository.LoadPage(pageNumber, numberOfElements);
        }

        public Order? FindById(int id)
        {
            return _orderRepository.FindById(id);
        }

        public Order? FindByName(string name)
        {
            Order? toFind = null;
            for (var i = 0; i < _orderRepository.GetCount(); i++)
            {
                var orders = _orderRepository.LoadPage(i, 100);
                toFind = orders.FirstOrDefault(x => x.CustomerName == name);
                if (toFind is not null)
                {
                    break;
                }
            }

            return toFind;
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

        public int GetCount()
        {
            return _orderRepository.GetCount();
        }
    }
}