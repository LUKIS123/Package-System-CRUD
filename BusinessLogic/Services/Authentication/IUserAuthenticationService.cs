using Package_System_CRUD.BusinessLogic.Enums;

namespace Package_System_CRUD.BusinessLogic.Services.Authentication
{
    public interface IUserAuthenticationService
    {
        public bool AuthenticateCustomer(string username);
        public bool AuthenticateManufacturer(string username);
        public bool AuthenticateEmployee(string username);
        public bool RegisterNewUser(string username, UserType userType);
        public string GetLoggedUsername();
        public int GetLoggedUserId();
        public UserType GetLoggedUserType();
    }
}