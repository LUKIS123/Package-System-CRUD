using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Repositories;

namespace Package_System_CRUD.BusinessLogic.Services
{
    public class ProductService : IModelService<Product>
    {
        private readonly IModelRepository<Product> _repository;

        public ProductService(IModelRepository<Product> repository)
        {
            _repository = repository;
        }

        public List<Product> GetPageList(int pageNumber, int numberOfElements)
        {
            return _repository.LoadPage(pageNumber, numberOfElements);
        }

        public Product? FindById(int id)
        {
            return _repository.FindById(id);
        }

        public Product? FindByName(string name)
        {
            return _repository.FindByName(name);
        }

        public void AddToDatabase(Product model)
        {
            _repository.SaveEntity(model);
        }

        public void RemoveFromDatabase(Product model)
        {
            _repository.DeleteEntity(model);
        }

        public void UpdateEntity(Product entity)
        {
            _repository.UpdateEntity(entity);
        }

        public int GetCount()
        {
            return _repository.GetCount();
        }
    }
}