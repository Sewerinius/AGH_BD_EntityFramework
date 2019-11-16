using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TZ_EntityFramework
{
    class Customer
    {
        [Key]
        public string CompanyName { get; set; }
        public string Description { get; set; }

        public List<Order> Orders { get; set; }

        public Customer()
        {
            Orders = new List<Order>();
        }
    }
}
