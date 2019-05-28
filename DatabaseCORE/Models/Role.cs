using System;
using System.Collections.Generic;

namespace DatabaseCORE.Models
{
    public partial class Role
    {
        public Role()
        {
            UserRole = new HashSet<UserRole>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }

        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
