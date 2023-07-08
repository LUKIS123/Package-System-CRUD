namespace Package_System_CRUD.BusinessLogic.Services
{
    public interface IModelService<T>
    {
        public List<T> GetPageList(int pageNumber, int numberOfElements);
        public T? FindById(int id);
        public T? FindByName(string name);
        public void AddToDatabase(T model);
        public void RemoveFromDatabase(T model);
        public void UpdateEntity(T entity);
    }
}