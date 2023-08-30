using Microsoft.EntityFrameworkCore;
using Package_System_CRUD.BusinessLogic.Data;
using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.BusinessLogic.Repositories
{
    public class OrderRepository : BaseModelRepository<Order>
    {
        public OrderRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        protected override DbSet<Order> GetTable()
        {
            return DbContext.Orders;
        }
    }
}