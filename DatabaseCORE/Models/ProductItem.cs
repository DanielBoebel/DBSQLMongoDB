using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseCORE.Models
{
	public class ProductItem
	{
		public ProductItem(string id, int productID, string productName, decimal price)
		{
			this.id = id;
			this.productID = productID;
			this.productName = productName;
			this.price = price;
		}

		public string id { get; set; }

		public int productID { get; set; }

		public string productName { get; set; }

		public decimal price { get; set; }
	}
}
