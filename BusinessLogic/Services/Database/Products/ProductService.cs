using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Repositories;

namespace Package_System_CRUD.BusinessLogic.Services.Database.Products
{
    public class ProductService : IProductService<Product>
    {
        private readonly IModelRepository<Product> _repository;

        public ProductService(IModelRepository<Product> repository)
        {
            _repository = repository;
        }

        public List<Product> GetPageList(int pageNumber, int numberOfElements)
        {
            return _repository
                .LoadPage(pageNumber, numberOfElements);
        }

        public Product? FindById(int id)
        {
            return _repository
                .FindById(x => x.Id == id);
        }

        public Product? FindByName(string name)
        {
            return _repository
                .FindByName(x => x.Name == name);
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

        public List<Product> GetFilteredByManufacturerId(int id)
        {
            return _repository
                .GetFiltered(
                    (order) => order.ManufacturerId == id
                );
        }

        public List<Product> GetFilteredByManufacturerId(int id, int pageNumber, int numberOfElements)
        {
            return _repository
                .GetFiltered(
                    (order) => order.ManufacturerId == id,
                    pageNumber,
                    numberOfElements
                );
        }
    }
}