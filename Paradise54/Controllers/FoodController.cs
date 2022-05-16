using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Paradise54.Controllers
{
  
    public class FoodController : Controller
    {
        FoodManager fm = new FoodManager(new EfFoodRepository());
     
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
    }
}
