using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TZ_EntityFramework
{
    class Product : INotifyPropertyChanged
    {
        public int ProductID { get; set; }
        public string Name { get; set; }

        private int _unitsInStock;
        public int UnitsInStock { get { return this._unitsInStock; } set { if (value != this._unitsInStock) { _unitsInStock = value; NotifyPropertyChanged(); } } }
        [ForeignKey("Category")]
        public int CategoryID { get; set; }
        public Category Category { get; set; }
        [Column(TypeName = "money")]
        public decimal UnitPrice { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Order placeOrder(Customer customer, int quantity)
        {
            UnitsInStock -= quantity;
            return new Order(this, customer, quantity);
        }
    }
}
