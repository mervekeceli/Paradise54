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
        public IActionResult Index()
        {
            var values = tm.GetList();
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
