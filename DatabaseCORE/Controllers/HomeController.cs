using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatabaseCORE.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Net;

namespace DatabaseCORE.Controllers
{
	public class HomeController : Controller
	{

		private DBWebshopContext db = new DBWebshopContext();
		public static List<Cart> cartList = new List<Cart>();
		public static int userID;

		public IActionResult Index()
		{
			userID = (int)HttpContext.Session.GetInt32("Id");
			ViewBag.userID = userID;
			ViewBag.session = true;
			GetProducts();
			var model = GetProducts();
			

			return View();
		}

		public IActionResult Products()
		{
			cartList = db.Cart.Where(c => c.UserId == userID ).ToList();
			ViewBag.session = true;
			return View(GetProducts());
		}


		public IActionResult addProducts(int productID, string productName, decimal price)
		{
			Cart cart = new Cart();
			if (cartList.Any(x => x.ProductId == productID))
			{
				var product = cartList.Where(cl => cl.ProductId == productID).FirstOrDefault();
				product.Quantity += 1;
				product.TotalAmount += price;
				db.Update(product);
				db.SaveChanges();
			}
			else
			{
				var id = userID;
				cart.ProductId = productID;
				cart.Productname = productName;
				cart.ProductPrice = price.ToString();
				cart.TotalAmount = price;
				cart.Quantity = 1;
				cart.UserId = (int)id;
				db.Add(cart);
				db.SaveChanges();

			}
			cartList.Add(cart);
			return RedirectToAction("Products","Home");
		}


		public IActionResult Cart()
		{
			ViewBag.session = true;
			return View(cartList);
		}


		public IActionResult CartEdit()
		{
			ViewBag.session = true;
			return RedirectToAction("Products", "Home");
		}

		public IActionResult CartDelete()
		{
			ViewBag.session = true;
			return RedirectToAction("Products", "Home");
		}



		[HttpPost]
		public IActionResult Index([Bind("Email")] Newsletter newsletter)
		{
			var subscribeBoolean = false;
			try {
				var subscribed = db.Newsletter.Where(n => n.Email == newsletter.Email).FirstOrDefault();
				var subscribedAcc = db.Newsletter.Where(n => n.Id == (int)HttpContext.Session.GetInt32("Id")).FirstOrDefault();

				if (subscribed == null && subscribedAcc == null)
				{
					subscribeBoolean = true;
				}
			}
			catch (Exception e)
			{

			}

			if(subscribeBoolean == true)
			{
				newsletter.Subscribed = 1;
				var userId = HttpContext.Session.GetInt32("ID");
				newsletter.Id = (int)userId;
				db.Add(newsletter);
				db.SaveChanges();
				ModelState.AddModelError("Email", "you are now subscribed!");
				ViewBag.subscribed = true;
			}
			else
			{
				ModelState.AddModelError("Email", "You're already subscribed ");
				ViewBag.subscribed = false;
			}


			return View();
		}
		public static List<ProductItem> GetProducts()
		{
			List<ProductItem> pi = new List<ProductItem>();
			string baseUrl = "https://localhost:44340/api/product";

			var syncClient = new WebClient();

			var content = syncClient.DownloadString(baseUrl);



			return JsonConvert.DeserializeObject<List<ProductItem>>(content);
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
