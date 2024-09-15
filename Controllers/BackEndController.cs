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
    public class BackEndController : Controller
    {
        private readonly ProjectDContext _context;

        public BackEndController(ProjectDContext context)
        {
            _context = context;
        }

        // GET: BackEnd
        public async Task<IActionResult> Index()
        {
            var projectDContext = _context.Admin.Include(a => a.sys);
            return View(await projectDContext.ToListAsync());
        }

        // GET: BackEnd/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin
                .Include(a => a.sys)
                .FirstOrDefaultAsync(m => m.aId == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // GET: BackEnd/Create
        public IActionResult Create()
        {
            ViewData["sysId"] = new SelectList(_context.SysAdmin, "sysId", "sysId");
            return View();
        }

        // POST: BackEnd/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("aId,Name,Gender,Birthday,cId,Password,sysId")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["sysId"] = new SelectList(_context.SysAdmin, "sysId", "sysId", admin.sysId);
            return View(admin);
        }

        // GET: BackEnd/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }
            ViewData["sysId"] = new SelectList(_context.SysAdmin, "sysId", "sysId", admin.sysId);
            return View(admin);
        }

        // POST: BackEnd/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("aId,Name,Gender,Birthday,cId,Password,sysId")] Admin admin)
        {
            if (id != admin.aId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(admin.aId))
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
            ViewData["sysId"] = new SelectList(_context.SysAdmin, "sysId", "sysId", admin.sysId);
            return View(admin);
        }

        // GET: BackEnd/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admin = await _context.Admin
                .Include(a => a.sys)
                .FirstOrDefaultAsync(m => m.aId == id);
            if (admin == null)
            {
                return NotFound();
            }

            return View(admin);
        }

        // POST: BackEnd/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var admin = await _context.Admin.FindAsync(id);
            if (admin != null)
            {
                _context.Admin.Remove(admin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminExists(string id)
        {
            return _context.Admin.Any(e => e.aId == id);
        }
    }
}
