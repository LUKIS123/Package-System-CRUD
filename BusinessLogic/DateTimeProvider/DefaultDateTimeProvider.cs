namespace Package_System_CRUD.BusinessLogic.DateTimeProvider
{
    public class DefaultDateTimeProvider : IDateTimeProvider
    {
        public DateTime GetDateTime()
        {
            return DateTime.Today;
        }
    }
}