using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatabaseCORE.Models;
using Newtonsoft;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;

namespace DatabaseCORE.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

		[HttpPost]
		public IActionResult Index([Bind("","","")]ProductItem productItem)
		{
			string output = JsonConvert.SerializeObject(productItem);
			var httpContent = new StringContent(output, Encoding.UTF8, "application/json");
			var httpClient = new HttpClient();
			string baseUrl = "https://localhost:44340/api/product";

			var httpResponse = httpClient.PostAsync(baseUrl, httpContent);


			return View();
		}
	}
}