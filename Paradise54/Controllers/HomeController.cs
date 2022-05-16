using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly Context _context;
        FoodManager fm = new FoodManager(new EfFoodRepository());

        public HomeController(Context context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
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

        public async Task<IActionResult> AddFoodToCart(int foodId,int tableNum)
        {
            var cart = await _context.Carts
                .Where(x => x.TableId == tableNum && x.Active == true)
                .FirstOrDefaultAsync();

            Food currentFood = await _context.Foods
                .Where(x => x.Id == foodId && x.Active == true)
                .FirstOrDefaultAsync();

            if (currentFood == null) return NotFound();

            if (cart != null)
            {
                CartItem newBasketItem = new CartItem
                {
                    CartId = cart.Id,
                    Food = currentFood,
                    Active = true
                };

                _context.Add(newBasketItem);
                await _context.SaveChangesAsync();
            }
            else
            {
                Cart newCart = new Cart
                {
                    Status = "YENI",
                    Active = true,
                    TableId = tableNum
                };

                _context.Add(newCart);
                _context.SaveChanges();

                CartItem newCartItem = new CartItem
                {
                    CartId = newCart.Id,
                    Food = currentFood,
                    Active = true
                };

                _context.Add(newCartItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Carts");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
