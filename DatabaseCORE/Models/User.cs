using System;
using System.Collections.Generic;

namespace DatabaseCORE.Models
{
    public partial class User
    {
        public User()
        {
            Cart = new HashSet<Cart>();
            Newsletter = new HashSet<Newsletter>();
            PaymentInformation = new HashSet<PaymentInformation>();
            UserRole = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Streetname { get; set; }
        public int UserZipcode { get; set; }
        public string Cityname { get; set; }

        public virtual ICollection<Cart> Cart { get; set; }
        public virtual ICollection<Newsletter> Newsletter { get; set; }
        public virtual ICollection<PaymentInformation> PaymentInformation { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
