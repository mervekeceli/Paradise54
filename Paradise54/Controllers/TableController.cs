using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paradise54.Controllers
{
    public class TableController : Controller
    {
        TableManager tm = new TableManager(new EfTableRepository());
        CartItemManager cim = new CartItemManager(new EfCartItemRepository());
        CartManager cm = new CartManager(new EfCartRepository());
        public IActionResult Index()
        {
           // List<CartItem> cartItems; // cim.GetCartItemListwithFoodCartIncludeFilter(tableNum);

                //if (cartItems.Count != 0)
                //{
                //    ViewData["ToplamFiyat"] = cartItems.Sum(x => x.Food.Price);
                //    ViewData["CartID"] = cartItems[0].CartId;
                //    return View(cartItems);
                //}

            var values = tm.GetList();
            List<double> totalPrice=new List<double>();
            for(int i=0;i<values.Count();i++)
            {
                List<CartItem> cartItems=cim.GetOrderListwithFoodCartIncludeFilter2(values[i].Id);
                if (cartItems.Count != 0)
                {
                    totalPrice.Add(cartItems.Sum(x => x.Food.Price));
                }
                //ViewBag.TotalPrice[i] = totalPrice[i];
            }
            ViewBag.TotalPrice = totalPrice;//dOGRU CALISIYOR.totalPrice[2] 3.MASANIN TOTALI DONUYOR
            ViewBag.totalCount = (totalPrice.Count())-1;
            return View(values);
        }

        /* Masa detayı görüntüleme fonksiyonu **/
        public IActionResult GetOrder(int tableId)
        {
            var values = cim.GetOrderListwithFoodCartIncludeFilter2(tableId);
            ViewBag.tableId = tableId;
            return View(values);
        }


        public IActionResult Details(int id)
        {
            //var mainCategory = await _context.MainCategories
            // .FirstOrDefaultAsync(m => m.Id == id);
            var table = tm.GetById(id);
            if (table == null)
            {
                return NotFound();
            }
            
            return View(table);
        }


        [HttpPost]
        public IActionResult Payment(int tableId)
        {
            Cart cart = cm.GetCartByTablenum(tableId);
            Table table = tm.GetById(tableId);
            table.Status = "BOS";
            tm.TUpdate(table);

            
            cart.Active = false;
            cart.Status = "TAMAMLANDI";
            cm.TUpdate(cart);

            return RedirectToAction("Index", "Table");
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Table table)
        {
            if (ModelState.IsValid)
            {
                table.Active = true;
                tm.TAdd(table);
                return RedirectToAction(nameof(Index));
            }
            return View(table);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {


            //var mainCategory = await _context.MainCategories.FindAsync(id);
            var table = tm.GetById(id);

            if (table == null)
            {
                return NotFound();
            }
            return View(table);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Status,Active")] Table table)
        {

            if (id != table.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    tm.TUpdate(table);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TableExists(table.Id))
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
            return View(table);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {

            var table = tm.GetById(id);
            //var table = await _context.MainCategories
            //    .FirstOrDefaultAsync(m => m.Id == id);
            if (table == null)
            {
                return NotFound();
            }

            return View(table);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            //MainCategory mainCategory = _context.MainCategories.Where(x => x.Id == id).FirstOrDefault();
            Table table = tm.GetById(id);

            if (table != null)
            {
                table.Active = !table.Active;
                tm.TUpdate(table);

            }
            else
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }


        private bool TableExists(int id)
        {
            //return _context.MainCategories.Any(e => e.Id == id);
            return tm.TExistss(id);
        }
    }
}
