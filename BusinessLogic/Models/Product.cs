using System.ComponentModel.DataAnnotations;

namespace Package_System_CRUD.BusinessLogic.Models
{
    public class Product
    {
        [Key] public int Id { get; set; }

        public int ManufacturerId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }
    }
}