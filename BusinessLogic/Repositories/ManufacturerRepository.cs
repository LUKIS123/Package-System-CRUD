using Package_System_CRUD.BusinessLogic.Data;
using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.BusinessLogic.Repositories
{
    public class ManufacturerRepository : BaseModelRepository<Manufacturer>
    {
        public ManufacturerRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        protected override IQueryable<Manufacturer> GetTable()
        {
            return DbContext.Manufacturers;
        }

        public override void SaveEntity(Manufacturer entity)
        {
            DbContext.Manufacturers.Add(entity);
            DbContext.SaveChanges();
        }

        public override void DeleteEntity(Manufacturer entity)
        {
            DbContext.Manufacturers.Remove(entity);
            DbContext.SaveChanges();
        }

        public override void UpdateEntity(Manufacturer entity)
        {
            DbContext.Manufacturers.Update(entity);
            DbContext.SaveChanges();
        }
    }
}