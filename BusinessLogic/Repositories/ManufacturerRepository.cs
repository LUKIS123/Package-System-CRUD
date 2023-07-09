using Package_System_CRUD.BusinessLogic.Data;
using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.BusinessLogic.Repositories
{
    public class ManufacturerRepository : IModelRepository<Manufacturer>
    {
        private readonly AppDbContext _dbContext;

        public ManufacturerRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public List<Manufacturer> LoadPage(int pageNumber, int numberOfElements)
        {
            return _dbContext
                .Manufacturers
                .Skip(pageNumber * numberOfElements)
                .Take(numberOfElements)
                .ToList();
        }

        public Manufacturer? FindById(int id)
        {
            return _dbContext
                .Manufacturers
                .FirstOrDefault(x => x.Id == id);
        }

        public void SaveEntity(Manufacturer entity)
        {
            _dbContext.Manufacturers.Add(entity);
            _dbContext.SaveChanges();
        }

        public void DeleteEntity(Manufacturer entity)
        {
            _dbContext.Manufacturers.Remove(entity);
            _dbContext.SaveChanges();
        }

        public void UpdateEntity(Manufacturer entity)
        {
            _dbContext.Manufacturers.Update(entity);
            _dbContext.SaveChanges();
        }

        public int GetCount()
        {
            return _dbContext.Manufacturers.Count();
        }
    }
}