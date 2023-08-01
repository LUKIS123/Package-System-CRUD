using Package_System_CRUD.BusinessLogic.Data;
using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.BusinessLogic.Repositories
{
    public class OrderRepository : IModelRepository<Order>
    {
        private readonly AppDbContext _dbContext;

        public OrderRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public List<Order> LoadPage(int pageNumber, int numberOfElements)
        {
            return _dbContext
                .Orders
                .Skip(pageNumber * numberOfElements)
                .Take(numberOfElements)
                .ToList();
        }

        public Order? FindById(int id)
        {
            return _dbContext
                .Orders
                .FirstOrDefault(x => x.Id == id);
        }

        public Order? FindByName(string name)
        {
            return _dbContext
                .Orders
                .FirstOrDefault(x => x.CustomerName == name);
        }

        public void SaveEntity(Order entity)
        {
            _dbContext.Orders.Add(entity);
            _dbContext.SaveChanges();
        }

        public void DeleteEntity(Order entity)
        {
            _dbContext.Orders.Remove(entity);
            _dbContext.SaveChanges();
        }

        public void UpdateEntity(Order entity)
        {
            _dbContext.Orders.Update(entity);
            _dbContext.SaveChanges();
        }

        public int GetCount()
        {
            return _dbContext.Orders.Count();
        }

        public List<Order> GetFiltered(Func<Order, bool> condition)
        {
            return _dbContext
                .Orders
                .Where(condition)
                .ToList();
        }

        public List<Order> GetFiltered(Func<Order, bool> condition, int pageNumber, int numberOfElements)
        {
            return _dbContext
                .Orders
                .Where(condition)
                .Skip(pageNumber * numberOfElements)
                .Take(numberOfElements)
                .ToList();
        }
    }
}