using Microsoft.EntityFrameworkCore;
using Package_System_CRUD.BusinessLogic.Data;
using Package_System_CRUD.BusinessLogic.Models;

namespace Package_System_CRUD.BusinessLogic.Repositories
{
    public class EmployeeRepository : BaseModelRepository<Employee>
    {
        public EmployeeRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        protected override DbSet<Employee> GetTable()
        {
            return DbContext.Employees;
        }
    }
}