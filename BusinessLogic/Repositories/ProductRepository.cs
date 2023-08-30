using Microsoft.EntityFrameworkCore;
using Package_System_CRUD.BusinessLogic.Data;
using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.BusinessLogic.Repositories
{
    public class ProductRepository : BaseModelRepository<Product>
    {
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        protected override DbSet<Product> GetTable()
        {
            return DbContext.Products;
        }
    }
}