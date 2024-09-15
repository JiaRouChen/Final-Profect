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
    public class withdrawalRecordsController : Controller
    {
        private readonly ProjectDContext _context;

        public withdrawalRecordsController(ProjectDContext context)
        {
            _context = context;
        }

        // GET: withdrawalRecords
        public async Task<IActionResult> Index()
        {
            var projectDContext = _context.withdrawalRecord.Include(w => w.pIdNavigation).Include(w => w.pNoNavigation).Include(w => w.sIdNavigation);
            return View(await projectDContext.ToListAsync());
        }

        // GET: withdrawalRecords/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var withdrawalRecord = await _context.withdrawalRecord
                .Include(w => w.pIdNavigation)
                .Include(w => w.pNoNavigation)
                .Include(w => w.sIdNavigation)
                .FirstOrDefaultAsync(m => m.sId == id);
            if (withdrawalRecord == null)
            {
                return NotFound();
            }

            return View(withdrawalRecord);
        }

        // GET: withdrawalRecords/Create
        public IActionResult Create()
        {
            ViewData["pId"] = new SelectList(_context.Product, "pId", "pId");
            ViewData["pNo"] = new SelectList(_context.ProcurmentSheet, "pNo", "pNo");
            ViewData["sId"] = new SelectList(_context.Supplier, "sId", "sId");
            return View();
        }

        // POST: withdrawalRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("sId,pId,pNo,quantity")] withdrawalRecord withdrawalRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(withdrawalRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["pId"] = new SelectList(_context.Product, "pId", "pId", withdrawalRecord.pId);
            ViewData["pNo"] = new SelectList(_context.ProcurmentSheet, "pNo", "pNo", withdrawalRecord.pNo);
            ViewData["sId"] = new SelectList(_context.Supplier, "sId", "sId", withdrawalRecord.sId);
            return View(withdrawalRecord);
        }

        // GET: withdrawalRecords/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var withdrawalRecord = await _context.withdrawalRecord.FindAsync(id);
            if (withdrawalRecord == null)
            {
                return NotFound();
            }
            ViewData["pId"] = new SelectList(_context.Product, "pId", "pId", withdrawalRecord.pId);
            ViewData["pNo"] = new SelectList(_context.ProcurmentSheet, "pNo", "pNo", withdrawalRecord.pNo);
            ViewData["sId"] = new SelectList(_context.Supplier, "sId", "sId", withdrawalRecord.sId);
            return View(withdrawalRecord);
        }

        // POST: withdrawalRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("sId,pId,pNo,quantity")] withdrawalRecord withdrawalRecord)
        {
            if (id != withdrawalRecord.sId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(withdrawalRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!withdrawalRecordExists(withdrawalRecord.sId))
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
            ViewData["pId"] = new SelectList(_context.Product, "pId", "pId", withdrawalRecord.pId);
            ViewData["pNo"] = new SelectList(_context.ProcurmentSheet, "pNo", "pNo", withdrawalRecord.pNo);
            ViewData["sId"] = new SelectList(_context.Supplier, "sId", "sId", withdrawalRecord.sId);
            return View(withdrawalRecord);
        }

        // GET: withdrawalRecords/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var withdrawalRecord = await _context.withdrawalRecord
                .Include(w => w.pIdNavigation)
                .Include(w => w.pNoNavigation)
                .Include(w => w.sIdNavigation)
                .FirstOrDefaultAsync(m => m.sId == id);
            if (withdrawalRecord == null)
            {
                return NotFound();
            }

            return View(withdrawalRecord);
        }

        // POST: withdrawalRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var withdrawalRecord = await _context.withdrawalRecord.FindAsync(id);
            if (withdrawalRecord != null)
            {
                _context.withdrawalRecord.Remove(withdrawalRecord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool withdrawalRecordExists(string id) => _context.withdrawalRecord.Any(e => e.sId == id);
    }
}
