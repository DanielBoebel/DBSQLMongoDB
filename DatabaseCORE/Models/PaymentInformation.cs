﻿using System;
using System.Collections.Generic;

namespace DatabaseCORE.Models
{
    public partial class PaymentInformation
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string CardOwnerName { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
