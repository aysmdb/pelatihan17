using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sportstore.Models;
using sportstore.Helper;

namespace sportstore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly Context _context;

        public ProductController(Context context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            int pageSize = 3;

            IQueryable<Product> products = _context.Product;

            PaginationHelper<Product> data = PaginationHelper<Product>.Create(products, pageNumber ?? 1, pageSize);

            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.CategoryList = _context.Category.Select(x => new SelectListItem {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name", "Description", "Price", "Category")] Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(product);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw e;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            Product product = await _context.Product.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id", "Name", "Description", "Price", "Category")] Product product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    throw e;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Product product = await _context.Product.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            try
            {
                _context.Remove(product);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}