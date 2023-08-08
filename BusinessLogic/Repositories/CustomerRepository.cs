using Package_System_CRUD.BusinessLogic.Data;
using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.BusinessLogic.Repositories
{
    public class CustomerRepository : BaseModelRepository<Customer>
    {
        public CustomerRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        protected override IQueryable<Customer> GetTable()
        {
            return DbContext.Customers;
        }

        public override void SaveEntity(Customer entity)
        {
            DbContext.Customers.Add(entity);
            DbContext.SaveChanges();
        }

        public override void DeleteEntity(Customer entity)
        {
            DbContext.Customers.Remove(entity);
            DbContext.SaveChanges();
        }

        public override void UpdateEntity(Customer entity)
        {
            DbContext.Customers.Update(entity);
            DbContext.SaveChanges();
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

        // public List<Customer> GetFilteredById(int foreignId)
        // {
        //     var customer = this.FindById(foreignId);
        //     return customer is null ? new List<Customer>() : new List<Customer> { customer };
        // }
    }
}