using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paradise54.Controllers
{
    public class TableController : Controller
    {
        TableManager tm = new TableManager(new EfTableRepository());
        public IActionResult Index()
        {
            var values = tm.GetList();
            return View(values);
        }
    }
}
