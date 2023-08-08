using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Package_System_CRUD.BusinessLogic.Models
{
    public class Order
    {
        [Key] public int Id { get; set; }

        [ForeignKey("Customer")] public required int CustomerId { get; set; }

        [ForeignKey("Manufacturer")] public required int ManufacturerId { get; set; }

        [ForeignKey("Product")] public required int ProductId { get; set; }

        public int Quantity { get; set; }

        public string? CustomerName { get; set; }

        [ForeignKey("OrderStatus")] public required OrderStatus Status { get; set; }

        public DateTime? SubmittedToEmployee { get; set; }

        public DateTime? SubmittedToManufacturer { get; set; }

        public DateTime? OrderRealized { get; set; }

        public DateTime? SentToCustomer { get; set; }

        public DateTime? Completed { get; set; }

        [NotMapped]
        public string Overview =>
            $"Product ID: {ProductId}\n" +
            $"Quantity: {Quantity}, " +
            $"Manufacturer ID: {ManufacturerId}\n" +
            $"Status: {Status.ToString()}";
    }
}