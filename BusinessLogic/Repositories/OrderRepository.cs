using Package_System_CRUD.BusinessLogic.Data;
using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.BusinessLogic.Repositories
{
    public class OrderRepository : BaseModelRepository<Order>
    {
        public OrderRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        protected override IQueryable<Order> GetTable()
        {
            return DbContext.Orders;
        }

        public override void SaveEntity(Order entity)
        {
            DbContext.Orders.Add(entity);
            DbContext.SaveChanges();
        }

        public override void DeleteEntity(Order entity)
        {
            DbContext.Orders.Remove(entity);
            DbContext.SaveChanges();
        }

        public override void UpdateEntity(Order entity)
        {
            DbContext.Orders.Update(entity);
            DbContext.SaveChanges();
        }
    }
}