﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paradise54.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
