using Package_System_CRUD.BusinessLogic.Data;
using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Repositories;

namespace Package_System_CRUD.BusinessLogic.Services
{
    public class ProductService : IModelService<Product>
    {
        private readonly ProductRepository _productRepository;

        public ProductService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> GetPageList(int pageNumber, int numberOfElements)
        {
            return _productRepository.LoadPage(pageNumber, numberOfElements);
        }

        public Product? FindById(int id)
        {
            return _productRepository.FindById(id);
        }

        public Product? FindByName(string name)
        {
            Product? toFind = null;
            for (var i = 0; i < _productRepository.GetCount(); i++)
            {
                var products = _productRepository.LoadPage(i, 100);
                toFind = products.FirstOrDefault(x => x.Name == name);
                if (toFind is not null)
                {
                    break;
                }
            }

            return toFind;
        }

        public void AddToDatabase(Product model)
        {
            _productRepository.SaveEntity(model);
        }

        public void RemoveFromDatabase(Product model)
        {
            _productRepository.DeleteEntity(model);
        }

        public void UpdateEntity(Product entity)
        {
            _productRepository.UpdateEntity(entity);
        }
    }
}