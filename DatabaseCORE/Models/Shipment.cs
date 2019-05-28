using System;
using System.Collections.Generic;

namespace DatabaseCORE.Models
{
    public partial class Shipment
    {
        public int ShipmentId { get; set; }
        public int OrderId { get; set; }
        public int InvoiceNumber { get; set; }
        public int ShipmentTrackingNumber { get; set; }
        public DateTime ShipmentDate { get; set; }

        public virtual Invoice InvoiceNumberNavigation { get; set; }
        public virtual Order Order { get; set; }
    }
}
