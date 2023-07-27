using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.BusinessLogic.Services
{
    public interface IOrderService<T>
    {
        public List<T> GetPageList(int pageNumber, int numberOfElements);
        public T? FindById(int id);
        public T? FindByName(string name);
        public void AddToDatabase(T model);
        public void RemoveFromDatabase(T model);
        public void UpdateEntity(T entity);
        public int GetCount();
        public List<T> GetFilteredByUserId(int id);
        public List<T> GetFilteredByManufacturerId(int id);
        public List<T> GetFilteredByStatus(OrderStatus orderStatus);
        public List<T> GetFilteredByUserId(int id, int pageNumber, int numberOfElements);
        public List<T> GetFilteredByManufacturerId(int id, int pageNumber, int numberOfElements);
        public List<T> GetFilteredByStatus(OrderStatus orderStatus, int pageNumber, int numberOfElements);
    }
}