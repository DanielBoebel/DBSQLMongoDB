using System;
using System.Collections.Generic;

namespace DatabaseCORE.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            Shipment = new HashSet<Shipment>();
        }

        public int InvoiceId { get; set; }
        public int OrderId { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string InvoiceDetails { get; set; }
        public int PaymentId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual ICollection<Shipment> Shipment { get; set; }
    }
}
