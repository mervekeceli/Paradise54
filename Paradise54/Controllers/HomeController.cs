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

        public IActionResult FoodDetails(int foodId, int tableNum)
        {
            List<Food> foods = fm.GetListRelatedFoods(foodId);//Sepete niye ihtiyac duyuluyor
            //var cart = cm.GetCartListFilter(tableNum);
            Food food = fm.GetById(foodId);
            if (food == null) return NotFound();
            foods.Add(food);
            ViewBag.MasaId = tableNum;
           

            return View(foods);
        }

        public IActionResult Foods(string? catName,string? searchItem)
        {
           
            var values = fm.GetFoodListWithCategory();
            if (!string.IsNullOrEmpty(catName))
            {
                values = fm.GetFoodListwithCategoryWithFilter(values, catName);
            }
            if(!string.IsNullOrEmpty(searchItem))
            {
                values = fm.GetSearchFoods(searchItem);
            }
            List<string> _categories = new List<string>();
            foreach (var item in values)
            {
                _categories.Add(item.Category.Name);
            }
            _categories = _categories.Distinct().ToList();

            ViewBag.Kategoriler = _categories;
           
            return View(values);
        }

        public IActionResult SearchProducts(string searchItem)//Kullanilmiyor
        {
            var foods = fm.GetSearchFoods(searchItem);
            return View(foods);
        }



        public IActionResult AddFoodToCart(int foodId, int tableNum,string? content)
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
                    Note="NULL",//yarin degisecek
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
                    Note = "NULL",//yarin degisecek
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
