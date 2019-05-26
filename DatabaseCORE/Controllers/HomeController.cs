using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatabaseCORE.Models;
using Microsoft.AspNetCore.Http;

using Microsoft.EntityFrameworkCore;
using System.Net.Http;

namespace DatabaseCORE.Controllers
{
	public class HomeController : Controller
	{

		private DBWebshopContext db = new DBWebshopContext();

		public IActionResult Index()
		{
			GetProduct();
			ViewBag.session = true;
			return View();
		}

		[HttpPost]
		public IActionResult Index([Bind("Email")] Newsletter newsletter)
		{
			var subscribeBoolean = false;
			try {
				var subscribed = db.Newsletter.Where(n => n.Email == newsletter.Email).FirstOrDefault();
				var subscribedAcc = db.Newsletter.Where(n => n.UserId == (int)HttpContext.Session.GetInt32("ID")).FirstOrDefault();

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
				newsletter.UserId = (int)userId;
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


		public static async void GetProduct()
		{
			string baseUrl = "https://localhost:44340/api/product";
			try
			{
				using (HttpClient client = new HttpClient())
				{
					using (HttpResponseMessage res = await client.GetAsync(baseUrl))
					{
						//Then get the data or content from the response in the next using statement, then within it you will get the data, and convert it to a c# object.
						using (HttpContent content = res.Content)
						{
							//Now assign your content to your data variable, by converting into a string using the await keyword.
							var data = await content.ReadAsStringAsync();
							//If the data isn't null return log convert the data using newtonsoft JObject Parse class method on the data.
							if (data != null)
							{
								//Now log your data in the console
								System.Diagnostics.Debug.WriteLine("data------------{0}", data);
							}
							else
							{
								System.Diagnostics.Debug.WriteLine("NO Data----------");
							}

						}
					}

				}
			} catch(Exception e)
			{
				Console.WriteLine(e);
			}
		}



		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
