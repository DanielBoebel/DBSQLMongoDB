using System;
using System.Collections.Generic;

namespace DatabaseCORE.Models
{
    public partial class PaymentInformation
    {
        public PaymentInformation()
        {
            Order = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string CardOwnerName { get; set; }
        public int UserId { get; set; }
        public int CartId { get; set; }
        public double AmountPayed { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
