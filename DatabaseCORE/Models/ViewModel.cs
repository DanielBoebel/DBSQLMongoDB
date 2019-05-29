using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseCORE.Models
{
	public class ViewModel
	{

		public Invoice invoice { get; set; }
		public User user { get; set; }
		public Order order { get; set; }
		public PaymentInformation paymentInformation { get; set; }
		public Payment payment { get; set; }
		public Shipment shipment { get; set; }



	}
}
