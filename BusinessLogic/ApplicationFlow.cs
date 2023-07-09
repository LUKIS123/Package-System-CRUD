using Package_System_CRUD.BusinessLogic.Data;
using Package_System_CRUD.BusinessLogic.Repositories;
using System.Diagnostics;
using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Services;

namespace Package_System_CRUD
{
    public class ApplicationFlow
    {
        private readonly AppDbContext _dbContext;
        // private readonly CustomerRepository _customerRepository;
        // private readonly ManufacturerRepository _manufacturerRepository;

        private readonly CustomerService _customerService;
        private readonly ManufacturerService _manufacturerService;

        public ApplicationFlow(AppDbContext dbContext, CustomerService customerService,
            ManufacturerService manufacturerService)
        {
            _dbContext = dbContext;
            _dbContext.Database.EnsureCreated();

            // _customerRepository = new CustomerRepository(_dbContext);
            // _manufacturerRepository = new ManufacturerRepository(_dbContext);

            _customerService = customerService;
            _manufacturerService = manufacturerService;
        }

        public bool CheckIfCustomerUsernameValid(string username)
        {
            return _customerService.FindByName(username) is not null;
        }


        public bool CheckIfManufacturerUsernameValid(string username)
        {
            return _manufacturerService.FindByName(username) is not null;
        }

        public bool RegisterNewUser(string username, bool isCustomerType)
        {
            if (isCustomerType)
            {
                if (_customerService.FindByName(username) is not null) return false;
                var newCustomer = new Customer { Username = username };
                _customerService.AddToDatabase(newCustomer);
                return true;
            }
            else
            {
                if (_manufacturerService.FindByName(username) is not null) return false;
                var newManufacturer = new Manufacturer { Name = username };
                _manufacturerService.AddToDatabase(newManufacturer);
                return true;
            }
        }

        public void Run()
        {
            // var customer = new Customer{Username = "Filip"};
            // _dbContext.Customers.Add(customer);
            // _dbContext.SaveChanges();
            //
            // var items = _customerRepository.LoadPage(0, 100);
            // foreach (var cus in items)
            // {
            //     Trace.WriteLine(cus.Username + "ID ->" + cus.Id);
            // }
            //
            // Trace.WriteLine($"List size={items.Count}");
            //
            // var sFile = System.IO.Path.Combine(
            //     Path.GetDirectoryName(Process.GetCurrentProcess().MainModule?.FileName) ??
            //     throw new Exception("File missing"), @".\date.txt"
            // );
            // var _dataFilePath = Path.GetFullPath(sFile);
            //
            // Trace.WriteLine(_dataFilePath);
        }
    }
}