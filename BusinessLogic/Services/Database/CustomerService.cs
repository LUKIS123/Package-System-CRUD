using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Repositories;

namespace Package_System_CRUD.BusinessLogic.Services.Database
{
    public class CustomerService : IModelService<Customer>
    {
        private readonly IModelRepository<Customer> _repository;

        public CustomerService(IModelRepository<Customer> customerRepository)
        {
            _repository = customerRepository;
        }

        public List<Customer> GetPageList(int pageNumber, int numberOfElements)
        {
            return _repository
                .LoadPage(pageNumber, numberOfElements);
        }

        public Customer? FindById(int id)
        {
            return _repository
                .FindById(x => x.Id == id);
        }

        public Customer? FindByName(string name)
        {
            return _repository
                .FindByName(x => x.Username == name);
        }

        public void AddToDatabase(Customer model)
        {
            _repository.SaveEntity(model);
        }

        public void RemoveFromDatabase(Customer model)
        {
            _repository.DeleteEntity(model);
        }

        public void UpdateEntity(Customer entity)
        {
            _repository.UpdateEntity(entity);
        }

        public int GetCount()
        {
            return _repository.GetCount();
        }
    }
}