using System;
using System.Collections.Generic;

namespace DatabaseCORE.Models
{
    public partial class Users
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Streetname { get; set; }
        public int Zipcode { get; set; }
        public string Cityname { get; set; }
    }
}
