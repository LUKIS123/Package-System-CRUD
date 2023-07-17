using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Package_System_CRUD.BusinessLogic.Models
{
    public class Customer
    {
        [Key] public int Id { get; set; }

        public string Username { get; set; }
    }
}