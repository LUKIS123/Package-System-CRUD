using System.ComponentModel.DataAnnotations;

namespace Package_System_CRUD.BusinessLogic.Models
{
    public class Customer
    {
        [Key] public int Id { get; set; }

        public string Username { get; set; }
    }
}