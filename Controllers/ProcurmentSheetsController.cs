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
    public class ProcurmentSheetsController : Controller
    {
        private readonly ProjectDContext _context;

        public ProcurmentSheetsController(ProjectDContext context)
        {
            _context = context;
        }

        // GET: ProcurmentSheets
        public async Task<IActionResult> Index()
        {
            var projectDContext = _context.ProcurmentSheet.Include(p => p.aIdNavigation);
            return View(await projectDContext.ToListAsync());
        }

        // GET: ProcurmentSheets/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procurmentSheet = await _context.ProcurmentSheet
                .Include(p => p.aIdNavigation)
                .FirstOrDefaultAsync(m => m.pNo == id);
            if (procurmentSheet == null)
            {
                return NotFound();
            }

            return View(procurmentSheet);
        }

        // GET: ProcurmentSheets/Create
        public IActionResult Create()
        {
            ViewData["aId"] = new SelectList(_context.Admin, "aId", "aId");
            return View();
        }

        // POST: ProcurmentSheets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("pNo,pDate,aId,pNote")] ProcurmentSheet procurmentSheet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(procurmentSheet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["aId"] = new SelectList(_context.Admin, "aId", "aId", procurmentSheet.aId);
            return View(procurmentSheet);
        }

        // GET: ProcurmentSheets/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procurmentSheet = await _context.ProcurmentSheet.FindAsync(id);
            if (procurmentSheet == null)
            {
                return NotFound();
            }
            ViewData["aId"] = new SelectList(_context.Admin, "aId", "aId", procurmentSheet.aId);
            return View(procurmentSheet);
        }

        // POST: ProcurmentSheets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("pNo,pDate,aId,pNote")] ProcurmentSheet procurmentSheet)
        {
            if (id != procurmentSheet.pNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(procurmentSheet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcurmentSheetExists(procurmentSheet.pNo))
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
            ViewData["aId"] = new SelectList(_context.Admin, "aId", "aId", procurmentSheet.aId);
            return View(procurmentSheet);
        }

        // GET: ProcurmentSheets/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var procurmentSheet = await _context.ProcurmentSheet
                .Include(p => p.aIdNavigation)
                .FirstOrDefaultAsync(m => m.pNo == id);
            if (procurmentSheet == null)
            {
                return NotFound();
            }

            return View(procurmentSheet);
        }

        // POST: ProcurmentSheets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var procurmentSheet = await _context.ProcurmentSheet.FindAsync(id);
            if (procurmentSheet != null)
            {
                _context.ProcurmentSheet.Remove(procurmentSheet);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProcurmentSheetExists(string id) => _context.ProcurmentSheet.Any(e => e.pNo == id);
    }
}
