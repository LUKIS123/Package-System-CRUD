using Package_System_CRUD.BusinessLogic.Data;
using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.BusinessLogic.Repositories
{
    public class ProductRepository : BaseModelRepository<Product>
    {
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        protected override IQueryable<Product> GetTable()
        {
            return DbContext.Products;
        }

        public override void SaveEntity(Product entity)
        {
            DbContext.Products.Add(entity);
            DbContext.SaveChanges();
        }

        public override void DeleteEntity(Product entity)
        {
            DbContext.Products.Remove(entity);
            DbContext.SaveChanges();
        }

        public override void UpdateEntity(Product entity)
        {
            DbContext.Products.Update(entity);
            DbContext.SaveChanges();
        }
    }
}