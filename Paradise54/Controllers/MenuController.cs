using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paradise54.Controllers
{
    public class MenuController : Controller
    {
        MenuManager mm = new MenuManager(new EfMenuRepository());
        public IActionResult Index()
        {
            var values = mm.GetList();
            return View(values);
        }
    }
}
