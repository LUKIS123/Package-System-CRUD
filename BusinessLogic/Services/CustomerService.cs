using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Repositories;

namespace Package_System_CRUD.BusinessLogic.Services
{
    public class CustomerService : IModelService<Customer>
    {
        private readonly CustomerRepository _repository;

        public CustomerService(CustomerRepository customerRepository)
        {
            _repository = customerRepository;
        }

        public List<Customer> GetPageList(int pageNumber, int numberOfElements)
        {
            return _repository.LoadPage(pageNumber, numberOfElements);
        }

        public Customer? FindById(int id)
        {
            return _repository.FindById(id);
        }

        public Customer? FindByName(string name)
        {
            Customer? toFind = null;
            for (var i = 0; i < Math.Ceiling((double)_repository.GetCount() / 100); i++)
            {
                var customers = _repository.LoadPage(i, 100);
                toFind = customers.FirstOrDefault(x => x.Username == name);
                if (toFind is not null)
                {
                    break;
                }
            }

            return toFind;
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
    }
}