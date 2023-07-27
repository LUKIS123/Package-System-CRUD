using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Package_System_CRUD.BusinessLogic.Models
{
    public class Product
    {
        [Key] public int Id { get; set; }

        [Required]
        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        [NotMapped]
        public string Overview =>
            $"Name={Name},\n" +
            $"Id={Id},\n" +
            $"ManufacturerId={ManufacturerId},\n" +
            $"Description={Description},\n" +
            $"Price={Price}";
    }
}