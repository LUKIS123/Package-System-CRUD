namespace Package_System_CRUD.BusinessLogic.Repositories
{
    public interface IModelRepository<T>
    {
        public List<T> LoadPage(int pageNumber, int numberOfElements);
        public T? FindById(int id);
        public void SaveEntity(T entity);
        public void DeleteEntity(T entity);
        public void UpdateEntity(T entity);
        public int GetCount();
    }
}