using System;
using System.Collections.Generic;

namespace DatabaseCORE.Models
{
    public partial class Order
    {
        public Order()
        {
            Invoice = new HashSet<Invoice>();
            Shipment = new HashSet<Shipment>();
        }

        public int Id { get; set; }
        public DateTime DateCompleted { get; set; }
        public decimal AmountToPay { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Invoice> Invoice { get; set; }
        public virtual ICollection<Shipment> Shipment { get; set; }
    }
}
