using Package_System_CRUD.BusinessLogic.Data;
using Package_System_CRUD.BusinessLogic.Models;
using Package_System_CRUD.BusinessLogic.Services;

namespace Package_System_CRUD.BusinessLogic
{
    public class UserAuthenticationService
    {
        private readonly IModelService<Customer> _customerService;
        private readonly IModelService<Manufacturer> _manufacturerService;
        public string? LoggedUser { get; set; }

        public UserAuthenticationService(
            AppDbContext dbContext,
            IModelService<Customer> customerService,
            IModelService<Manufacturer> manufacturerService
        )
        {
            dbContext.Database.EnsureCreated();

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
    }
}