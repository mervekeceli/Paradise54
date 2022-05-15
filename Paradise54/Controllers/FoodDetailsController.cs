using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paradise54.Controllers
{
    public class FoodDetailsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
