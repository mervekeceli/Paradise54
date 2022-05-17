using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Paradise54.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Paradise54.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        FoodManager fm = new FoodManager(new EfFoodRepository());
        CartManager cm = new CartManager(new EfCartRepository());
        CartItemManager cim = new CartItemManager(new EfCartItemRepository());

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FoodDetails()
        {
            return View();
        }

        public IActionResult Foods()
        {
            var values = fm.GetList();
            return View(values);
        }

        public IActionResult AddFoodToCart(int foodId, int tableNum)
        {

            var cart = cm.GetById(tableNum);
            Food currentFood = fm.GetById(foodId);

            if (currentFood == null) return NotFound();

            if (cart != null)
            {

                CartItem newCartItem = new CartItem
                {
                    CartId = cart.Id,
                    FoodId = currentFood.Id,
                    Active = true
                };
                cim.TAdd(newCartItem);

            }
            else
            {
                Cart newCart = new Cart
                {
                    Status = "YENI",
                    Active = true,
                    TableId = tableNum
                };
                cm.TAdd(newCart);

                CartItem newCartItem = new CartItem
                {
                    CartId = newCart.Id,
                    FoodId = currentFood.Id,
                    Active = true
                };
                cim.TAdd(newCartItem);
            }

            return RedirectToAction("Index", "Cart", new { tableNum = tableNum });
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
