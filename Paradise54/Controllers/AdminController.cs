using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paradise54.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public PartialViewResult AdminPartialNav()
        {
            return PartialView();
        }

        public PartialViewResult AdminPartialHeader()
        {
            return PartialView();
        }
    }
}
