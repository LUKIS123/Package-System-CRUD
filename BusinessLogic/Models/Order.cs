using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Package_System_CRUD.BusinessLogic.Models
{
    public class Order
    {
        [Key] public int Id { get; set; }

        [Required] [ForeignKey("Customer")] public int CustomerId { get; set; }

        [Required]
        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }

        [Required] [ForeignKey("Product")] public int ProductId { get; set; }

        public int Quantity { get; set; }

        public string? CustomerName { get; set; }

        [Required] [ForeignKey("OrderStatus")] public OrderStatus Status { get; set; }

        public DateTime? SubmittedToEmployee { get; set; }

        public DateTime? SubmittedToManufacturer { get; set; }

        public DateTime? OrderRealized { get; set; }

        public DateTime? SentToCustomer { get; set; }

        public DateTime? Completed { get; set; }
    }
}