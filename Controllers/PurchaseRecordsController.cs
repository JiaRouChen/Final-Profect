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
    public class PurchaseRecordsController : Controller
    {
        private readonly ProjectDContext _context;

        public PurchaseRecordsController(ProjectDContext context)
        {
            _context = context;
        }

        // GET: PurchaseRecords
        public async Task<IActionResult> Index()
        {
            var projectDContext = _context.PurchaseRecord.Include(p => p.pIdNavigation).Include(p => p.sIdNavigation).Include(p => p.sNoNavigation);
            return View(await projectDContext.ToListAsync());
        }

        // GET: PurchaseRecords/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseRecord = await _context.PurchaseRecord
                .Include(p => p.pIdNavigation)
                .Include(p => p.sIdNavigation)
                .Include(p => p.sNoNavigation)
                .FirstOrDefaultAsync(m => m.sId == id);
            if (purchaseRecord == null)
            {
                return NotFound();
            }

            return View(purchaseRecord);
        }

        // GET: PurchaseRecords/Create
        public IActionResult Create()
        {
            ViewData["pId"] = new SelectList(_context.Product, "pId", "pId");
            ViewData["sId"] = new SelectList(_context.Supplier, "sId", "sId");
            ViewData["sNo"] = new SelectList(_context.Stocksheet, "sNo", "sNo");
            return View();
        }

        // POST: PurchaseRecords/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("sId,pId,sNo,quantity")] PurchaseRecord purchaseRecord)
        {
            if (ModelState.IsValid)
            {
                _context.Add(purchaseRecord);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["pId"] = new SelectList(_context.Product, "pId", "pId", purchaseRecord.pId);
            ViewData["sId"] = new SelectList(_context.Supplier, "sId", "sId", purchaseRecord.sId);
            ViewData["sNo"] = new SelectList(_context.Stocksheet, "sNo", "sNo", purchaseRecord.sNo);
            return View(purchaseRecord);
        }

        // GET: PurchaseRecords/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseRecord = await _context.PurchaseRecord.FindAsync(id);
            if (purchaseRecord == null)
            {
                return NotFound();
            }
            ViewData["pId"] = new SelectList(_context.Product, "pId", "pId", purchaseRecord.pId);
            ViewData["sId"] = new SelectList(_context.Supplier, "sId", "sId", purchaseRecord.sId);
            ViewData["sNo"] = new SelectList(_context.Stocksheet, "sNo", "sNo", purchaseRecord.sNo);
            return View(purchaseRecord);
        }

        // POST: PurchaseRecords/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("sId,pId,sNo,quantity")] PurchaseRecord purchaseRecord)
        {
            if (id != purchaseRecord.sId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchaseRecord);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseRecordExists(purchaseRecord.sId))
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
            ViewData["pId"] = new SelectList(_context.Product, "pId", "pId", purchaseRecord.pId);
            ViewData["sId"] = new SelectList(_context.Supplier, "sId", "sId", purchaseRecord.sId);
            ViewData["sNo"] = new SelectList(_context.Stocksheet, "sNo", "sNo", purchaseRecord.sNo);
            return View(purchaseRecord);
        }

        // GET: PurchaseRecords/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var purchaseRecord = await _context.PurchaseRecord
                .Include(p => p.pIdNavigation)
                .Include(p => p.sIdNavigation)
                .Include(p => p.sNoNavigation)
                .FirstOrDefaultAsync(m => m.sId == id);
            if (purchaseRecord == null)
            {
                return NotFound();
            }

            return View(purchaseRecord);
        }

        // POST: PurchaseRecords/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var purchaseRecord = await _context.PurchaseRecord.FindAsync(id);
            if (purchaseRecord != null)
            {
                _context.PurchaseRecord.Remove(purchaseRecord);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseRecordExists(string id) => _context.PurchaseRecord.Any(e => e.sId == id);
    }
}
