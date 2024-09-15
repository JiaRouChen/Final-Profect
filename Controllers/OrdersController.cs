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
    public class OrdersController : Controller
    {
        private readonly ProjectDContext _context;

        public OrdersController(ProjectDContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            var projectDContext = _context.Orders.Include(o => o.Mem).Include(o => o.aIdNavigation);
            return View(await projectDContext.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Mem)
                .Include(o => o.aIdNavigation)
                .FirstOrDefaultAsync(m => m.orderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["MemId"] = new SelectList(_context.Member, "MemId", "MemId");
            ViewData["aId"] = new SelectList(_context.Admin, "aId", "aId");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("orderId,MemId,aId,orderDate,requiredDate,shippedDate,Freight,SName")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemId"] = new SelectList(_context.Member, "MemId", "MemId", orders.MemId);
            ViewData["aId"] = new SelectList(_context.Admin, "aId", "aId", orders.aId);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders.FindAsync(id);
            if (orders == null)
            {
                return NotFound();
            }
            ViewData["MemId"] = new SelectList(_context.Member, "MemId", "MemId", orders.MemId);
            ViewData["aId"] = new SelectList(_context.Admin, "aId", "aId", orders.aId);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("orderId,MemId,aId,orderDate,requiredDate,shippedDate,Freight,SName")] Orders orders)
        {
            if (id != orders.orderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersExists(orders.orderId))
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
            ViewData["MemId"] = new SelectList(_context.Member, "MemId", "MemId", orders.MemId);
            ViewData["aId"] = new SelectList(_context.Admin, "aId", "aId", orders.aId);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.Mem)
                .Include(o => o.aIdNavigation)
                .FirstOrDefaultAsync(m => m.orderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var orders = await _context.Orders.FindAsync(id);
            if (orders != null)
            {
                _context.Orders.Remove(orders);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersExists(string id) => _context.Orders.Any(e => e.orderId == id);
    }
}
