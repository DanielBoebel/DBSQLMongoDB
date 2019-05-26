using System;
using System.Collections.Generic;

namespace DatabaseCORE.Models
{
    public partial class Newsletter
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public byte Subscribed { get; set; }
        public string Email { get; set; }

        public virtual User User { get; set; }
    }
}
