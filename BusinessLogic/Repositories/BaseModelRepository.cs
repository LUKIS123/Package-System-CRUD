using Microsoft.EntityFrameworkCore;
using Package_System_CRUD.BusinessLogic.Data;

namespace Package_System_CRUD.BusinessLogic.Repositories
{
    public abstract class BaseModelRepository<T> : IModelRepository<T> where T : class
    {
        protected readonly AppDbContext DbContext;

        protected BaseModelRepository(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected abstract DbSet<T> GetTable();

        public void SaveEntity(T entity)
        {
            GetTable().Add(entity);
            DbContext.SaveChanges();
        }

        public void DeleteEntity(T entity)
        {
            GetTable().Remove(entity);
            DbContext.SaveChanges();
        }

        public void UpdateEntity(T entity)
        {
            GetTable().Update(entity);
            DbContext.SaveChanges();
        }

        public T? FindById(Func<T, bool> idGetter)
        {
            return GetTable()
                .FirstOrDefault(idGetter);
        }

        public T? FindByName(Func<T, bool> nameGetter)
        {
            return GetTable()
                .FirstOrDefault(nameGetter);
        }

        public List<T> LoadPage(int pageNumber, int numberOfElements)
        {
            return GetTable()
                .Skip(pageNumber * numberOfElements)
                .Take(numberOfElements)
                .ToList();
        }

        public List<T> GetFiltered(Func<T, bool> condition)
        {
            return GetTable()
                .Where(condition)
                .ToList();
        }

        public List<T> GetFiltered(Func<T, bool> condition, int pageNumber, int numberOfElements)
        {
            return GetTable()
                .Where(condition)
                .Skip(pageNumber * numberOfElements)
                .Take(numberOfElements)
                .ToList();
        }
    }
}