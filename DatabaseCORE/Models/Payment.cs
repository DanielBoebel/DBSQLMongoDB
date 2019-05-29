using System;
using System.Collections.Generic;

namespace DatabaseCORE.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Invoice = new HashSet<Invoice>();
        }

        public int PaymentId { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public int PaymentInfomrationId { get; set; }

        public virtual PaymentInformation PaymentInfomration { get; set; }
        public virtual ICollection<Invoice> Invoice { get; set; }
    }
}
