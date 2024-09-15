using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using D05Project.Models;
using D05Project.ViewModels;

namespace D05Project.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProjectDContext _context;

        public ProductsController(ProjectDContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(string  catId="0001")
        {
            //var northwindContext = _context.Products.Include(p => p.Category).Include(p => p.Supplier);

            VMProduct Product = new VMProduct()
             {
                 Product = await _context.Product.Where(c => c.catId == catId).ToListAsync(),
                 Category = await _context.Category.ToListAsync()
             };


            ViewData["catName"] = _context.Category.Find(catId).catName;
            ViewData["catId"] = catId;

            return View(Product);
        }



        // GET: Products
        //public async Task<IActionResult> Index()
        //{
        //    var projectDContext = _context.Product.Include(p => p.cat).Include(p => p.sIdNavigation);
        //    return View(await projectDContext.ToListAsync());
        //}

        // GET: Products/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.cat)
                .Include(p => p.sIdNavigation)
                .FirstOrDefaultAsync(m => m.pId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["catId"] = new SelectList(_context.Category, "catId", "catId");
            ViewData["sId"] = new SelectList(_context.Supplier, "sId", "sId");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("sId,pId,unit,saveQuantity,quantity,prodoctName,catId,prodoctPrice")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["catId"] = new SelectList(_context.Category, "catId", "catId", product.catId);
            ViewData["sId"] = new SelectList(_context.Supplier, "sId", "sId", product.sId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["catId"] = new SelectList(_context.Category, "catId", "catId", product.catId);
            ViewData["sId"] = new SelectList(_context.Supplier, "sId", "sId", product.sId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("sId,pId,unit,saveQuantity,quantity,prodoctName,catId,prodoctPrice")] Product product)
        {
            if (id != product.pId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.pId))
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
            ViewData["catId"] = new SelectList(_context.Category, "catId", "catId", product.catId);
            ViewData["sId"] = new SelectList(_context.Supplier, "sId", "sId", product.sId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.cat)
                .Include(p => p.sIdNavigation)
                .FirstOrDefaultAsync(m => m.pId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(string id) => _context.Product.Any(e => e.pId == id);
    }
}
