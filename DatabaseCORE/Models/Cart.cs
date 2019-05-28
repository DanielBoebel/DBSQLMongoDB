using System;
using System.Collections.Generic;

namespace DatabaseCORE.Models
{
    public partial class Cart
    {
        public int Id { get; set; }
        public string Productname { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public decimal? TotalAmount { get; set; }
        public int? OrderId { get; set; }
        public string ProductPrice { get; set; }
        public int UserId { get; set; }

        public virtual Order Order { get; set; }
        public virtual User User { get; set; }
    }
}
