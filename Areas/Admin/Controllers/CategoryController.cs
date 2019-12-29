using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using sportstore.Models;

namespace sportstore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly Context _context;

        public CategoryController(Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Category> categories = _context.Category.ToList();

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("Name")] Category category)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Add(category);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                catch(Exception e)
                {
                    throw e;
                }
            }

            return View();
        }

        public IActionResult Edit(int id)
        {
            Category category = _context.Category.Where(x => x.Id == id).FirstOrDefault();

            if(category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit([Bind("Id", "Name")] Category category)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index));
                }
                catch(Exception e)
                {
                    throw e;
                }
            }

            return View(category);
        }

        public IActionResult Delete(int id)
        {
            Category category = _context.Category.FirstOrDefault(x => x.Id == id);

            if(category == null)
            {
                return NotFound();
            }

            try
            {
                _context.Remove(category);
                _context.SaveChanges();
            }
            catch(Exception e)
            {
                throw e;
            }

            return RedirectToAction(nameof(Index));
        }     
    }
}