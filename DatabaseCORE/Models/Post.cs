using System;
using System.Collections.Generic;

namespace DatabaseCORE.Models
{
    public partial class Post
    {
        public int Id { get; set; }
        public int? Postnummer { get; set; }
        public string By { get; set; }
    }
}
