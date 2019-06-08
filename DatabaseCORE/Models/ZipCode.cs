using System;
using System.Collections.Generic;

namespace DatabaseCORE.Models
{
    public partial class ZipCode
    {
        public int ZipCode1 { get; set; }
        public string City { get; set; }
        public string Municipality { get; set; }
        public string District { get; set; }
        public string Province { get; set; }
    }
}
