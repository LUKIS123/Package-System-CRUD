﻿namespace Package_System_CRUD.BusinessLogic.DateTimeProvider
{
    public interface IDateTimeProvider
    {
        public DateTime RefreshCurrentDateTime();
        public DateTime GetDateTime();
    }
}