using System;
using System.Collections.Generic;

namespace DatabaseCORE.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public DateTime DateCompleted { get; set; }
        public int PaymentId { get; set; }
        public int CartId { get; set; }
        public int UserId { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual PaymentInformation Payment { get; set; }
        public virtual User User { get; set; }
    }
}
