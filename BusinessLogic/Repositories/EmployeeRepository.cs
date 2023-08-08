using Package_System_CRUD.BusinessLogic.Data;
using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.BusinessLogic.Repositories
{
    public class EmployeeRepository : BaseModelRepository<Employee>
    {
        public EmployeeRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        protected override IQueryable<Employee> GetTable()
        {
            return DbContext.Employees;
        }

        public override void SaveEntity(Employee entity)
        {
            DbContext.Employees.Add(entity);
            DbContext.SaveChanges();
        }

        public override void DeleteEntity(Employee entity)
        {
            DbContext.Employees.Remove(entity);
            DbContext.SaveChanges();
        }

        public override void UpdateEntity(Employee entity)
        {
            DbContext.Employees.Update(entity);
            DbContext.SaveChanges();
        }
    }
}