using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Package_System_CRUD.BusinessLogic.Models
{
    public class Product
    {
        [Key] public int Id { get; set; }

        [ForeignKey("Manufacturer")] public required int ManufacturerId { get; set; }

        public required string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        [NotMapped]
        public string Overview =>
            $"Name: {Name},\n" +
            $"ManufacturerId: {ManufacturerId},\n" +
            $"Price: {Price}\n";
    }
}