using Package_System_CRUD.BusinessLogic.Data;

namespace Package_System_CRUD.BusinessLogic.Repositories
{
    public abstract class BaseModelRepository<T> : IModelRepository<T>
    {
        protected readonly AppDbContext DbContext;

        protected BaseModelRepository(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected abstract IQueryable<T> GetTable();

        public abstract void SaveEntity(T entity);

        public abstract void DeleteEntity(T entity);

        public abstract void UpdateEntity(T entity);

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

        public int GetCount()
        {
            return GetTable()
                .Count();
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
                .AsEnumerable()
                .Where(condition)
                .ToList();
        }

        public List<T> GetFiltered(Func<T, bool> condition, int pageNumber, int numberOfElements)
        {
            return GetTable()
                .AsEnumerable()
                .Where(condition)
                .Skip(pageNumber * numberOfElements)
                .Take(numberOfElements)
                .ToList();
        }
    }
}