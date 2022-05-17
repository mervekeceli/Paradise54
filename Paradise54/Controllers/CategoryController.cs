using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Paradise54.Controllers
{
    public class CategoryController : Controller
    {
        CategoryManager cm = new CategoryManager(new EfCategoryRepository());
        public IActionResult Index()
        {
            var values = cm.GetList();
            return View(values);
        }

        public IActionResult Details(int id)
        {

            var category = cm.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Create()
        {
           // ViewData["CategoryId"] = new SelectList(cm.GetList().Where(x => x.Active == true), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.Active = true;
                cm.TAdd(category);
                return RedirectToAction(nameof(Index));
            }

            //ViewData["MainCategoryId"] = new SelectList(_context.MainCategories, "Id", "Name", category.MainCategoryId);

            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {

            //var category = await _context.Categories.FindAsync(id);
            var category = cm.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            //ViewData["MainCategoryId"] = new SelectList(_context.MainCategories.Where(a => a.Active == true), "Id", "Name", category.MainCategoryId);

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    cm.TUpdate(category);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
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



            //ViewData["MainCategoryId"] = new SelectList(_context.MainCategories, "Id", "Name", category.MainCategoryId);

            return View(category);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {


            //var category = await _context.Categories
            //    .Include(b => b.MainCategory)
            //    .FirstOrDefaultAsync(m => m.Id == id);
            var category = cm.GetById(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            //Category category = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
            Category category = cm.GetById(id);


            if (category != null)
            {
                category.Active = !category.Active;
                cm.TUpdate(category);
            }
            else
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            //return _context.Categories.Any(e => e.Id == id);
            return cm.TExistss(id);
            
        }


    }
}
