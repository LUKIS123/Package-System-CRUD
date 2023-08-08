namespace Package_System_CRUD.BusinessLogic.Repositories
{
    public interface IModelRepository<T>
    {
        public void SaveEntity(T entity);
        public void DeleteEntity(T entity);
        public void UpdateEntity(T entity);
        public T? FindById(Func<T, bool> idGetter);
        public T? FindByName(Func<T, bool> nameGetter);
        public int GetCount();
        public List<T> LoadPage(int pageNumber, int numberOfElements);
        public List<T> GetFiltered(Func<T, bool> condition);
        public List<T> GetFiltered(Func<T, bool> condition, int pageNumber, int numberOfElements);
    }
}