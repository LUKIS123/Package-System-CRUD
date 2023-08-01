using Package_System_CRUD.BusinessLogic.Data;
using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.BusinessLogic.Repositories
{
    public class ProductRepository : IModelRepository<Product>
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Product> LoadPage(int pageNumber, int numberOfElements)
        {
            return _dbContext
                .Products
                .Skip(pageNumber * numberOfElements)
                .Take(numberOfElements)
                .ToList();
        }

        public Product? FindById(int id)
        {
            return _dbContext
                .Products
                .FirstOrDefault(x => x.Id == id);
        }

        public Product? FindByName(string name)
        {
            return _dbContext
                .Products
                .FirstOrDefault(x => x.Name == name);
        }

        public void SaveEntity(Product entity)
        {
            _dbContext.Products.Add(entity);
            _dbContext.SaveChanges();
        }

        public void DeleteEntity(Product entity)
        {
            _dbContext.Products.Remove(entity);
            _dbContext.SaveChanges();
        }

        public void UpdateEntity(Product entity)
        {
            _dbContext.Products.Update(entity);
            _dbContext.SaveChanges();
        }

        public int GetCount()
        {
            return _dbContext.Products.Count();
        }

        public List<Product> GetFiltered(Func<Product, bool> condition)
        {
            return _dbContext
                .Products
                .Where(condition)
                .ToList();
        }

        public List<Product> GetFiltered(Func<Product, bool> condition, int pageNumber, int numberOfElements)
        {
            return _dbContext
                .Products
                .Where(condition)
                .Skip(pageNumber * numberOfElements)
                .Take(numberOfElements)
                .ToList();
        }
    }
}