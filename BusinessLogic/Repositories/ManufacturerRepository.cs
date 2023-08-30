using Microsoft.EntityFrameworkCore;
using Package_System_CRUD.BusinessLogic.Data;
using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.BusinessLogic.Repositories
{
    public class ManufacturerRepository : BaseModelRepository<Manufacturer>
    {
        public ManufacturerRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        protected override DbSet<Manufacturer> GetTable()
        {
            return DbContext.Manufacturers;
        }
    }
}