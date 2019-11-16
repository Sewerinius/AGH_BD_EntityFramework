using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TZ_EntityFramework
{
    class Order : INotifyPropertyChanged
    {
        private int _orderID;
        [Key]
        public int OrderID { get { return this._orderID; } set { if (this._orderID != value) { this._orderID = value; NotifyPropertyChanged(); } } }

        [ForeignKey("Product")]
        public int ProductID { get; set; }
        public Product Product { get; set; }

        [ForeignKey("Customer")]
        public string CompanyName { get; set; }
        public Customer Customer { get; set; }

        public int Quantity { get; set; }
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }
        public DateTime Date { get; set; }

        [NotMapped]
        public decimal TotalPrice { get { return Quantity * UnitPrice; } }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Order()
        {

        }

        public Order(Product product, Customer customer, int quantity)
        {
            Product = product;
            Customer = customer;
            UnitPrice = product.UnitPrice;
            Date = DateTime.Now;
            Quantity = quantity;
        }
    }
}
