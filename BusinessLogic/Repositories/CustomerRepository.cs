using Package_System_CRUD.BusinessLogic.Data;
using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.BusinessLogic.Repositories
{
    public class CustomerRepository : IModelRepository<Customer>
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

        public Customer? FindByName(string name)
        {
            return _dbContext
                .Customers
                .FirstOrDefault(x => x.Username == name);
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

        public List<Customer> GetFiltered(Func<Customer, bool> condition)
        {
            return _dbContext
                .Customers
                .Where(condition)
                .ToList();
        }

        public List<Customer> GetFiltered(Func<Customer, int, bool> condition)
        {
            return _dbContext
                .Customers
                .Where(condition)
                .ToList();
        }

        public List<Customer> GetFiltered(Func<Customer, bool> condition, int pageNumber, int numberOfElements)
        {
            return _dbContext
                .Customers
                .Where(condition)
                .Skip(pageNumber * numberOfElements)
                .Take(numberOfElements)
                .ToList();
        }

        // public List<Customer> GetFiltered(Func<Customer, string, bool> condition)
        // {
        //     var t = new Func<Customer, string, bool>((Customer x, string name) => x.Username == name);
        //
        //     return _dbContext
        //         .Customers
        //         .Where(t)
        //         .ToList();
        // }

        //
        // public List<Customer> GetFilteredById(int foreignId)
        // {
        //     var customer = this.FindById(foreignId);
        //     return customer is null ? new List<Customer>() : new List<Customer> { customer };
        // }
    }
}