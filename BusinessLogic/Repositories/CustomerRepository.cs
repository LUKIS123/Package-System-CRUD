using Package_System_CRUD.BusinessLogic.Data;
using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.BusinessLogic.Repositories
{
    internal class CustomerRepository : IModelRepository<Customer>
    {
        private readonly AppDbContext _dbContext;

        public CustomerRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public List<Customer> LoadPage(int pageNumber, int numberOfElements)
        {
            return _dbContext
                .Customers
                .Skip(pageNumber * numberOfElements)
                .Take(numberOfElements)
                .ToList();
        }

        public Customer? FindById(int id)
        {
            return _dbContext
                .Customers
                .FirstOrDefault(x => x.Id == id);
        }

        public void SaveEntity(Customer entity)
        {
            _dbContext.Customers.Add(entity);
            _dbContext.SaveChanges();
        }

        public void DeleteEntity(Customer entity)
        {
            _dbContext.Customers.Remove(entity);
            _dbContext.SaveChanges();
        }

        public void UpdateEntity(Customer entity)
        {
            _dbContext.Customers.Update(entity);
            _dbContext.SaveChanges();
        }

        public int GetCount()
        {
            return _dbContext.Customers.Count();
        }
    }
}