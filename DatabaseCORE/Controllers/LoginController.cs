using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseCORE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;


namespace DatabaseCORE.Controllers
{
    public class LoginController : Controller
    {

		private DBWebshopContext db = new DBWebshopContext();


		public IActionResult Index()
        {
			ViewBag.session = false;
			return View();
        }

		[HttpPost]
		public IActionResult Index(string username, string password)
		{
			List<string> usernameDB = null;
			List<string> passwordDB = null;
			usernameDB = db.User.Where(x => x.Username == username).Select(x => x.Username).ToList();
			var IdDB = db.User.Where(x => x.Username == username).Select(x => x.Id).FirstOrDefault();

			passwordDB = db.User.Where(x => x.Username == username).Select(x => x.Password).ToList();

			if (username.Equals(usernameDB[0]) && password.Equals(passwordDB[0]))
			{
				HttpContext.Session.SetInt32("Id", IdDB); 
				return RedirectToAction("Index", "Home");
			}
			else
			{
				return View();
			}
		}

		public IActionResult Signup()
		{
			ViewBag.session = false;
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Signup([Bind("Username,Password,Streetname,UserZipcode")] User user)
		{
			UserRole userRole = new UserRole();
			

			var uniqueUser = db.User.Where(x => x.Username == user.Username).Select(x=>x.Username).ToList();
			var cityname = db.ZipCode.Where(x => x.ZipCode1 == user.UserZipcode).Select(x => x.City).ToList();
			if (cityname.Any())
			{
				user.Cityname = cityname[0];
			}
			else
			{
				ViewBag.error = "The zip code dosent exist";
				return View();
			}

			if (!uniqueUser.Any())
			{

				if (ModelState.IsValid)
				{
					db.Add(user);
					await db.SaveChangesAsync();
					return RedirectToAction(nameof(Index));
				}
			}
			else
			{
				ViewBag.error = "The username already exist";
				return View();
			}
			return View();
		}
	}
}