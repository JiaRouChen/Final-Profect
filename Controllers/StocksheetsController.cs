using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using D05Project.Models;

namespace D05Project.Controllers
{
    public class StocksheetsController : Controller
    {
        private readonly ProjectDContext _context;

        public StocksheetsController(ProjectDContext context)
        {
            _context = context;
        }

        // GET: Stocksheets
        public async Task<IActionResult> Index()
        {
            var projectDContext = _context.Stocksheet.Include(s => s.aIdNavigation);
            return View(await projectDContext.ToListAsync());
        }

        // GET: Stocksheets/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stocksheet = await _context.Stocksheet
                .Include(s => s.aIdNavigation)
                .FirstOrDefaultAsync(m => m.sNo == id);
            if (stocksheet == null)
            {
                return NotFound();
            }

            return View(stocksheet);
        }

        // GET: Stocksheets/Create
        public IActionResult Create()
        {
            ViewData["aId"] = new SelectList(_context.Admin, "aId", "aId");
            return View();
        }

        // POST: Stocksheets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("sNo,sDate,aId,sNote")] Stocksheet stocksheet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stocksheet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["aId"] = new SelectList(_context.Admin, "aId", "aId", stocksheet.aId);
            return View(stocksheet);
        }

        // GET: Stocksheets/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stocksheet = await _context.Stocksheet.FindAsync(id);
            if (stocksheet == null)
            {
                return NotFound();
            }
            ViewData["aId"] = new SelectList(_context.Admin, "aId", "aId", stocksheet.aId);
            return View(stocksheet);
        }

        // POST: Stocksheets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("sNo,sDate,aId,sNote")] Stocksheet stocksheet)
        {
            if (id != stocksheet.sNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stocksheet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StocksheetExists(stocksheet.sNo))
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
            ViewData["aId"] = new SelectList(_context.Admin, "aId", "aId", stocksheet.aId);
            return View(stocksheet);
        }

        // GET: Stocksheets/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stocksheet = await _context.Stocksheet
                .Include(s => s.aIdNavigation)
                .FirstOrDefaultAsync(m => m.sNo == id);
            if (stocksheet == null)
            {
                return NotFound();
            }

            return View(stocksheet);
        }

        // POST: Stocksheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var stocksheet = await _context.Stocksheet.FindAsync(id);
            if (stocksheet != null)
            {
                _context.Stocksheet.Remove(stocksheet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StocksheetExists(string id) => _context.Stocksheet.Any(e => e.sNo == id);
    }
}
