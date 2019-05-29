using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseCORE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseCORE.Controllers
{
    public class CartController : Controller
    {

		private DBWebshopContext db = new DBWebshopContext();
		public static List<Cart> cartList = new List<Cart>();
		public static int userID;

		// GET: Cart
		public ActionResult Index()
        {
			userID = (int)HttpContext.Session.GetInt32("Id");
			ViewBag.amount = db.Cart.Where(ca => ca.UserId == userID).Sum(c => c.TotalAmount);
			cartList = db.Cart.Where(c => c.UserId == userID).ToList();
			ViewBag.session = true;
			return View(cartList);
		}



        // GET: Cart/Delete/5
		[HttpGet]
        public ActionResult Delete(int id)
        {
			var productRemove = db.Cart.Where(c => c.Id == id).FirstOrDefault();
			return View(productRemove);
        }

        // POST: Cart/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection col)
        {
            try
            {
				var deleteCart = db.Cart.Where(c => c.Id == id).FirstOrDefault();
				db.Remove(deleteCart);
				db.SaveChanges();

                return RedirectToAction("Index", "Cart");
            }
            catch
            {
                return View();
            }
        }


		public ActionResult Payment()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Payment([Bind("CardNumber","CardOwnerName")]PaymentInformation paymentInformation)
		{

			paymentInformation.UserId = userID;


			Order order = new Order();
			order.DateCompleted = DateTime.Now;
			order.AmountToPay = (decimal)db.Cart.Where(ca => ca.UserId == userID).Sum(c => c.TotalAmount);
			order.UserId = userID;

			paymentInformation.UserId = userID;
			db.Add(order);
			db.Add(paymentInformation);
			db.SaveChanges();
			removeCart(userID);

			return RedirectToAction("Index","Invoice",userID);
		}


		public void removeCart(int Id)
		{
			var cart = db.Cart.Where(c => c.UserId == Id).ToList();
			foreach (var item in cart)
			{
				db.Remove(item);
				db.SaveChanges();
			}

		}

    }
}