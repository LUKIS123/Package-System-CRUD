using Package_System_CRUD.BusinessLogic.Data;
using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Services;

namespace Package_System_CRUD.BusinessLogic
{
    public class UserAuthenticationService
    {
        private readonly IModelService<Customer> _customerService;
        private readonly IModelService<Manufacturer> _manufacturerService;
        private readonly IModelService<Employee> _employeeService;

        public string LoggedUser { get; private set; } = string.Empty;
        public int LoggedUserId { get; private set; }
        public UserType UserType { get; private set; } = UserType.NotLoggedIn;

        public UserAuthenticationService(
            AppDbContext dbContext,
            IModelService<Customer> customerService,
            IModelService<Manufacturer> manufacturerService,
            IModelService<Employee> employeeService
        )
        {
            dbContext.Database.EnsureCreated();

            _customerService = customerService;
            _manufacturerService = manufacturerService;
            _employeeService = employeeService;
        }

        public bool AuthenticateCustomer(string username)
        {
            var found = _customerService.FindByName(username);
            if (found is null) return false;
            StashUserData(username, found.Id, UserType.Customer);
            return true;
        }

        public bool AuthenticateManufacturer(string username)
        {
            var found = _manufacturerService.FindByName(username);
            if (found is null) return false;
            StashUserData(username, found.Id, UserType.Manufacturer);
            return true;
        }

        public bool AuthenticateEmployee(string username)
        {
            var found = _employeeService.FindByName(username);
            if (found is null) return false;
            StashUserData(username, found.Id, UserType.Employee);
            return true;
        }

        private void StashUserData(string username, int id, UserType userType)
        {
            LoggedUser = username;
            LoggedUserId = id;
            UserType = userType;
        }

        public bool RegisterNewUser(string username, UserType userType)
        {
            switch (userType)
            {
                case UserType.Customer when _customerService.FindByName(username) is not null:
                    return false;
                case UserType.Customer:
                {
                    var newCustomer = new Customer { Username = username };
                    _customerService.AddToDatabase(newCustomer);
                    return true;
                }
                case UserType.Manufacturer when _manufacturerService.FindByName(username) is not null:
                    return false;
                case UserType.Manufacturer:
                {
                    var newManufacturer = new Manufacturer { Name = username };
                    _manufacturerService.AddToDatabase(newManufacturer);
                    return true;
                }
                case UserType.Employee when _employeeService.FindByName(username) is not null:
                    return false;
                case UserType.Employee:
                {
                    var newEmployee = new Employee { Username = username };
                    _employeeService.AddToDatabase(newEmployee);
                    return true;
                }
                case UserType.NotLoggedIn:
                default:
                    return false;
            }
        }
    }
}