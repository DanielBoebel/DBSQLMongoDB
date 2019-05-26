using System;
using System.Collections.Generic;

namespace DatabaseCORE.Models
{
    public partial class Cart
    {
        public Cart()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string Productname { get; set; }
        public int Quantity { get; set; }
        public int Productid { get; set; }
        public string TotalAmount { get; set; }
        public int Userid { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
