using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Package_System_CRUD.BusinessLogic.Models
{
    public class Order : INotifyPropertyChanged
    {
        private int _customerId;
        private int _manufacturerId;
        private int _productId;
        private int _quantity;
        private string? _customerName;
        private OrderStatus _status;
        private DateTime _submittedToEmployee;
        private DateTime _submittedToManufacturer;
        private DateTime _orderRealized;
        private DateTime _sentToCustomer;
        private DateTime _completed;

        [Key] public int Id { get; set; }

        [Required]
        [ForeignKey("Customer")]
        public int CustomerId
        {
            get => _customerId;
            set
            {
                _customerId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CustomerId)));
            }
        }

        [Required]
        [ForeignKey("Manufacturer")]
        public int ManufacturerId
        {
            get => _manufacturerId;
            set
            {
                _manufacturerId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ManufacturerId)));
            }
        }

        public int ProductId
        {
            get => _productId;
            set
            {
                _productId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProductId)));
            }
        }

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Quantity)));
            }
        }

        public string? CustomerName
        {
            get => _customerName;
            set
            {
                _customerName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CustomerName)));
            }
        }

        public OrderStatus Status
        {
            get => _status;
            set
            {
                _status = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Status)));
            }
        }

        public DateTime SubmittedToEmployee
        {
            get => _submittedToEmployee;
            set
            {
                _submittedToEmployee = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SubmittedToEmployee)));
            }
        }

        public DateTime SubmittedToManufacturer
        {
            get => _submittedToManufacturer;
            set
            {
                _submittedToManufacturer = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SubmittedToManufacturer)));
            }
        }

        public DateTime OrderRealized
        {
            get => _orderRealized;
            set
            {
                _orderRealized = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OrderRealized)));
            }
        }

        public DateTime SentToCustomer
        {
            get => _sentToCustomer;
            set
            {
                _sentToCustomer = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SentToCustomer)));
            }
        }

        public DateTime Completed
        {
            get => _completed;
            set
            {
                _completed = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Completed)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}