using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Repositories;

namespace Package_System_CRUD.BusinessLogic.Services
{
    public class EmployeeService : IModelService<Employee>
    {
        private readonly IModelRepository<Employee> _repository;

        public EmployeeService(IModelRepository<Employee> employeeRepository)
        {
            _repository = employeeRepository;
        }

        public List<Employee> GetPageList(int pageNumber, int numberOfElements)
        {
            return _repository.LoadPage(pageNumber, numberOfElements);
        }

        public Employee? FindById(int id)
        {
            return _repository.FindById(id);
        }

        public Employee? FindByName(string name)
        {
            return _repository.FindByName(name);
        }

        public void AddToDatabase(Employee model)
        {
            _repository.SaveEntity(model);
        }

        public void RemoveFromDatabase(Employee model)
        {
            _repository.DeleteEntity(model);
        }

        public void UpdateEntity(Employee entity)
        {
            _repository.UpdateEntity(entity);
        }

        public int GetCount()
        {
            return _repository.GetCount();
        }
    }
}