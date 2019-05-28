using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseCORE.Models;

namespace DatabaseCORE.Models
{
	public class UserContext : DbContext
	{
		public DbSet<User> User{ get; set; }
	}
}
