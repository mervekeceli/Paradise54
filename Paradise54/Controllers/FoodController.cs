using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Paradise54.Controllers
{
    [AllowAnonymous]
    public class FoodController : Controller
    {
        FoodManager fm = new FoodManager(new EfFoodRepository());
        CategoryManager cm = new CategoryManager(new EfCategoryRepository());

        
        public IActionResult Index()
        {
            var values = fm.GetFoodListWithCategory();
            return View(values);
        }
        public Category GetCategory(string categoryName)
        {
            
            using (var c = new Context())
            {
                var values = c.Categories.Where(x => x.Name==categoryName).FirstOrDefault();
                return values;
            }
            
        }
        public async Task<ViewResult> AddFoodwithAPIAsync()
        {

            var values = fm.GetList();
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://6280b7d51020d8520580af3e.mockapi.io/api/paradise54/foods"),

            };
            List<FoodCopy> myDeserializedClass;
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                dynamic obj = JsonConvert.DeserializeObject(body);

                myDeserializedClass = JsonConvert.DeserializeObject<List<FoodCopy>>(body);


            }
            for (int i = 0; i <= myDeserializedClass.Count() - 1; i++)
            {
                Food f = new Food();
                f.Name = myDeserializedClass[i].Name;
                f.Photo = myDeserializedClass[i].Photo;
                f.Price = myDeserializedClass[i].Price;
                f.Ingredients = myDeserializedClass[i].Ingredients;
                f.Type = myDeserializedClass[i].Type;
                f.CategoryId= GetCategory(myDeserializedClass[i].Category).Id;
                f.Active = myDeserializedClass[i].Active;
                

                fm.TAdd(f);
            }
            return View();
        }


        public IActionResult Details(int id)
        {
            //var brand = await _context.Brands.Include(b => b.ApplicationUser)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var food = fm.GetById(id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }


        [HttpGet]
        public IActionResult Create()
        {
            
            ViewData["CategoryId"] = new SelectList(cm.GetList().Where(x => x.Active == true), "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Photo,Price,Stock,Ingredients,Type,Active,Note")] Food food)
        {
            if (ModelState.IsValid)
            {
                food.Active = true;
                fm.TAdd(food);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(cm.GetList().Where(x => x.Active == true), "Id", "Name", food.CategoryId);//Hatali bence
            return View(food);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {

            var food = fm.GetById(id);//await _context.Brands.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Description,Active")] Food food)
        {

            if (id != food.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    fm.TUpdate(food);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodExists(food.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(food);
        }



        [HttpGet]
        public IActionResult Delete(int id)
        {

            //var brand = await _context.Brands
            //    .FirstOrDefaultAsync(m => m.Id == id);
            Food food = fm.GetById(id);
            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            //Brand brand = _context.Brands.Where(x => x.Id == id).FirstOrDefault();
            Food food = fm.GetById(id);

            if (food != null)
            {
                food.Active = !food.Active;
                fm.TUpdate(food);

            }
            else
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool FoodExists(int id)
        {
            //return _context.Brands.Any(e => e.Id == id);
            return fm.TExistss(id);
        }
    }
}
