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

		// GET: Cart
		public ActionResult Index()
        {
			cartList = db.Cart.Where(c => c.UserId == HttpContext.Session.GetInt32("Id")).ToList();
			ViewBag.session = true;
			return View(cartList);
		}


        // GET: Cart/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cart/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
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
    }
}