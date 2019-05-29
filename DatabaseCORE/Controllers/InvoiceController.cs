using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseCORE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseCORE.Controllers
{
    public class InvoiceController : Controller
    {
		private DBWebshopContext db = new DBWebshopContext();
		public static int userID;

		public IActionResult Index(int id)
        {
			userID = (int)HttpContext.Session.GetInt32("Id");

			ViewBag.amountToPay = db.Order.Where(o => o.UserId == userID).OrderByDescending(d => d.DateCompleted).Select(a => a.AmountToPay).FirstOrDefault();
			ViewBag.session = true;
			
			return View();
        }

		[HttpPost]
		public IActionResult Index()
		{
			Payment payment = new Payment();
			payment.PaymentAmount = db.Order.Where(o => o.UserId == userID).OrderByDescending(d => d.DateCompleted ).Select(a => a.AmountToPay).FirstOrDefault();
			payment.PaymentDate = DateTime.Now;
			payment.PaymentInfomrationId = db.PaymentInformation.Where(pi => pi.UserId == userID).Select(i => i.Id).FirstOrDefault();
			db.Add(payment);
			db.SaveChanges();

			Order order = new Order();
			order.Id = db.Order.Where(o => o.UserId == userID).Select(oid => oid.Id).FirstOrDefault();



			Invoice invoice = new Invoice();
			invoice.OrderId = order.Id;
			int paymentInformationID = db.PaymentInformation.Where(pi => pi.UserId == userID).Select(i => i.Id).FirstOrDefault();
			invoice.PaymentId = db.Payment.Where(p => p.PaymentInfomrationId == paymentInformationID).Select(p => p.PaymentId).FirstOrDefault();
			invoice.InvoiceDate = DateTime.Now;
			invoice.InvoiceDetails = "This is great details";
			db.Add(invoice);
			db.SaveChanges();


			Shipment shipment = new Shipment();
			shipment.OrderId = order.Id;
			shipment.InvoiceNumber = db.Invoice.Where(i => i.OrderId == order.Id).Select(inumb => inumb.InvoiceId).FirstOrDefault();
			Random rnd = new Random();
			shipment.ShipmentTrackingNumber = rnd.Next(10000000, 1000000000);
			shipment.ShipmentDate = DateTime.Now.AddDays(1);

			db.Add(shipment);
			db.SaveChanges();


			return RedirectToAction("Invoice", "Invoice");
		}

		public IActionResult Invoice()
		{

			Payment payment = new Payment();
			payment.PaymentAmount = db.Order.Where(o => o.UserId == userID).OrderByDescending(d => d.DateCompleted).Select(a => a.AmountToPay).FirstOrDefault();
			payment.PaymentDate = DateTime.Now;
			payment.PaymentInfomrationId = db.PaymentInformation.Where(pi => pi.UserId == userID).OrderByDescending(d => d.Id).Select(i => i.Id).FirstOrDefault();
			db.Add(payment);
			db.SaveChanges();

			Order order = new Order();
			order.Id = db.Order.Where(o => o.UserId == userID).Select(oid => oid.Id).FirstOrDefault();



			Invoice invoice = new Invoice();
			invoice.OrderId = order.Id;
			int paymentInformationID = db.PaymentInformation.Where(pi => pi.UserId == userID).Select(i => i.Id).FirstOrDefault();
			invoice.PaymentId = db.Payment.Where(p => p.PaymentInfomrationId == paymentInformationID).Select(p => p.PaymentId).FirstOrDefault();
			invoice.InvoiceDate = DateTime.Now;
			invoice.InvoiceDetails = "This is great details";
			db.Add(invoice);
			db.SaveChanges();


			Shipment shipment = new Shipment();
			shipment.OrderId = order.Id;
			shipment.InvoiceNumber = db.Invoice.Where(i => i.OrderId == order.Id).Select(inumb => inumb.InvoiceId).FirstOrDefault();
			Random rnd = new Random();
			shipment.ShipmentTrackingNumber = rnd.Next(10000000, 1000000000);
			shipment.ShipmentDate = DateTime.Now.AddDays(1);

			db.Add(shipment);
			db.SaveChanges();

			ViewModel vm = new ViewModel();
			int orderID = db.Order.Where(o => o.UserId == userID).Select(o => o.Id).FirstOrDefault();
			vm.invoice = db.Invoice.Where(i => i.OrderId == orderID).FirstOrDefault();
			vm.user = db.User.Where(u => u.Id == userID).FirstOrDefault();
			vm.order = db.Order.Where(o => o.UserId == userID).FirstOrDefault();


			return View(vm);
		}
    }
}