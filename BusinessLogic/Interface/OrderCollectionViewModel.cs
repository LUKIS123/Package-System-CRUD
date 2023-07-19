using Package_System_CRUD.BusinessLogic.Models;
using System.ComponentModel;

namespace Package_System_CRUD.BusinessLogic.Interface
{
    public class OrderCollectionViewModel : INotifyPropertyChanged
    {
        private int _id;
        private string? _customerName;
        private string? _manufacturerName;
        private string? _productName;
        private int _quantity;
        private OrderStatus _status;
        private DateTime? _submittedToEmployee;
        private DateTime? _submittedToManufacturer;
        private DateTime? _orderRealized;
        private DateTime? _sentToCustomer;
        private DateTime? _completed;
        public event PropertyChangedEventHandler? PropertyChanged;

        public int Id
        {
            get => _id;
            set
            {
                _id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Id)));
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

        public string? ManufacturerName
        {
            get => _manufacturerName;
            set
            {
                _manufacturerName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ManufacturerName)));
            }
        }

        public string? ProductName
        {
            get => _productName;
            set
            {
                _productName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProductName)));
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

        public OrderStatus Status
        {
            get => _status;
            set
            {
                _status = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Status)));
            }
        }

        public DateTime? SubmittedToEmployee
        {
            get => _submittedToEmployee;
            set
            {
                _submittedToEmployee = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SubmittedToEmployee)));
            }
        }

        public DateTime? SubmittedToManufacturer
        {
            get => _submittedToManufacturer;
            set
            {
                _submittedToManufacturer = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SubmittedToManufacturer)));
            }
        }

        public DateTime? OrderRealized
        {
            get => _orderRealized;
            set
            {
                _orderRealized = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OrderRealized)));
            }
        }

        public DateTime? SentToCustomer
        {
            get => _sentToCustomer;
            set
            {
                _sentToCustomer = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SentToCustomer)));
            }
        }

        public DateTime? Completed
        {
            get => _completed;
            set
            {
                _completed = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Completed)));
            }
        }

        public string Overview =>
            $"Product: {ProductName}\n" +
            $"Quantity: {Quantity}, " +
            $"Manufacturer: {ManufacturerName}\n" +
            $"Status: {Status.ToString()}";

        public string Details =>
            $"Product: {ProductName}\n" +
            $"Quantity: {Quantity}," +
            $" Manufacturer: {ManufacturerName}\n" +
            $"Status: {Status.ToString()}\n\n" +
            $"Order ID: {Id}\n" +
            $"SubmittedToEmployee: {String.Format("{0:dd-MM-yyyy}", SubmittedToEmployee)}\n" +
            $"SubmittedToManufacturer: {String.Format("{0:dd-MM-yyyy}", SubmittedToManufacturer)}\n" +
            $"OrderRealized: {String.Format("{0:dd-MM-yyyy}", OrderRealized)}\n" +
            $"SentToCustomer: {String.Format("{0:dd-MM-yyyy}", SentToCustomer)}\n" +
            $"Completed: {String.Format("{0:dd-MM-yyyy}", Completed)}";
    }
}