using Package_System_CRUD.BusinessLogic.Data;
using Package_System_CRUD.BusinessLogic.Repositories;
using System.Diagnostics;

namespace Package_System_CRUD
{
    public class ApplicationFlow
    {
        private readonly AppDbContext _dbContext;
        private readonly CustomerRepository _customerRepository;

        public ApplicationFlow(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();

            _customerRepository = new CustomerRepository(_dbContext);
        }

        public void Run()
        {
            // var customer = new Customer{Username = "Filip"};
            // _dbContext.Customers.Add(customer);
            // _dbContext.SaveChanges();

            var items = _customerRepository.LoadPage(0, 100);
            foreach (var cus in items)
            {
                Trace.WriteLine(cus.Username + "ID ->" + cus.Id);
            }

            Trace.WriteLine($"List size={items.Count}");

            var sFile = System.IO.Path.Combine(
                Path.GetDirectoryName(Process.GetCurrentProcess().MainModule?.FileName) ??
                throw new Exception("File missing"), @".\date.txt"
            );
            var _dataFilePath = Path.GetFullPath(sFile);

            Trace.WriteLine(_dataFilePath);
        }
    }
}