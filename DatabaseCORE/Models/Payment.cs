using System;
using System.Collections.Generic;

namespace DatabaseCORE.Models
{
    public partial class Payment
    {
        public int PaymentId { get; set; }
        public int InvoiceId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }

        public virtual Invoice Invoice { get; set; }
    }
}
