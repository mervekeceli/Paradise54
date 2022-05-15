using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
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
        //public async Task<IActionResult> IndexAsync()
        //{
        //    var values = fm.GetList();
        //    var client = new HttpClient();
        //    var request = new HttpRequestMessage
        //    {
        //        Method = HttpMethod.Get,
        //        RequestUri = new Uri("https://6280b7d51020d8520580af3e.mockapi.io/api/paradise54/foods"),

        //    };
        //    List<Food> myDeserializedClass;
        //    using (var response = await client.SendAsync(request))
        //    {
        //        response.EnsureSuccessStatusCode();
        //        var body = await response.Content.ReadAsStringAsync();

        //        dynamic obj = JsonConvert.DeserializeObject(body);

        //        myDeserializedClass= JsonConvert.DeserializeObject<List<Food>>(body);


        //    }
        //    for (int i = 0; i <= myDeserializedClass.Count() - 1; i++)
        //    {
        //        Food f = new Food();
        //        f.Name = myDeserializedClass[i].Name;
        //        f.Photo = myDeserializedClass[i].Photo;
        //        f.Price = myDeserializedClass[i].Price;
        //        f.Ingredients = myDeserializedClass[i].Ingredients;
        //        f.Active = myDeserializedClass[i].Active;

        //        fm.TAdd(f);
        //    }
        //    return View(values);
        //}
        public IActionResult Index()
        {
            var values = fm.GetList();
            return View(values);
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
            List<Food> myDeserializedClass;
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();

                dynamic obj = JsonConvert.DeserializeObject(body);

                myDeserializedClass = JsonConvert.DeserializeObject<List<Food>>(body);


            }
            for (int i = 0; i <= myDeserializedClass.Count() - 1; i++)
            {
                Food f = new Food();
                f.Name = myDeserializedClass[i].Name;
                f.Photo = myDeserializedClass[i].Photo;
                f.Price = myDeserializedClass[i].Price;
                f.Ingredients = myDeserializedClass[i].Ingredients;
                f.Active = myDeserializedClass[i].Active;

                fm.TAdd(f);
            }
            return View();
        }
    }
}
