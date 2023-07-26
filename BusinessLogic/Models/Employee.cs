using System.ComponentModel.DataAnnotations;

namespace Package_System_CRUD.BusinessLogic.Models
{
    public class Employee
    {
        [Key] public int Id { get; set; }

        public string Username { get; set; }
    }
}