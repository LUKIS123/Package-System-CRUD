using Microsoft.EntityFrameworkCore;
using Package_System_CRUD.BusinessLogic.Data;
using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.BusinessLogic.Repositories
{
    public class CustomerRepository : BaseModelRepository<Customer>
    {
        public CustomerRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        protected override DbSet<Customer> GetTable()
        {
            return DbContext.Customers;
        }
    }
}