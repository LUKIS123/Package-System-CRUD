using Package_System_CRUD.BusinessLogic.Data;
using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.BusinessLogic.Repositories
{
    public class EmployeeRepository : IModelRepository<Employee>
    {
        private readonly AppDbContext _dbContext;

        public EmployeeRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Employee> LoadPage(int pageNumber, int numberOfElements)
        {
            return _dbContext
                .Employees
                .Skip(pageNumber * numberOfElements)
                .Take(numberOfElements)
                .ToList();
        }

        public Employee? FindById(int id)
        {
            return _dbContext
                .Employees
                .FirstOrDefault(x => x.Id == id);
        }

        public Employee? FindByName(string name)
        {
            return _dbContext
                .Employees
                .FirstOrDefault(x => x.Username == name);
        }

        public void SaveEntity(Employee entity)
        {
            _dbContext.Employees.Add(entity);
            _dbContext.SaveChanges();
        }

        public void DeleteEntity(Employee entity)
        {
            _dbContext.Employees.Remove(entity);
            _dbContext.SaveChanges();
        }

        public void UpdateEntity(Employee entity)
        {
            throw new NotImplementedException();
        }

        public int GetCount()
        {
            return _dbContext.Employees.Count();
        }

        public List<Employee> GetFiltered(Func<Employee, bool> condition)
        {
            return _dbContext
                .Employees
                .Where(condition)
                .ToList();
        }

        public List<Employee> GetFiltered(Func<Employee, bool> condition, int pageNumber, int numberOfElements)
        {
            return _dbContext
                .Employees
                .Where(condition)
                .Skip(pageNumber * numberOfElements)
                .Take(numberOfElements)
                .ToList();
        }
    }
}