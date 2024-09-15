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
    public class SysAdminsController : Controller
    {
        private readonly ProjectDContext _context;

        public SysAdminsController(ProjectDContext context)
        {
            _context = context;
        }

        // GET: SysAdmins
        public async Task<IActionResult> Index() => View(await _context.SysAdmin.ToListAsync());

        // GET: SysAdmins/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sysAdmin = await _context.SysAdmin
                .FirstOrDefaultAsync(m => m.sysId == id);
            if (sysAdmin == null)
            {
                return NotFound();
            }

            return View(sysAdmin);
        }

        // GET: SysAdmins/Create
        public IActionResult Create() => View();

        // POST: SysAdmins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("sysId,sysname,sysgender,syscId,syspassword")] SysAdmin sysAdmin)
        {
            if (ModelState.IsValid)
            {
                // sysAdmin.sysbirthday 已经在构造函数中设置了默认值
                _context.Add(sysAdmin);
                await _context.SaveChangesAsync();

                // 重定向到外部页面
                return Redirect("http://localhost:5230/order-confirmation.html");
            }
            return View(sysAdmin);
        }

        // GET: SysAdmins/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sysAdmin = await _context.SysAdmin.FindAsync(id);
            if (sysAdmin == null)
            {
                return NotFound();
            }
            return View(sysAdmin);
        }

        // POST: SysAdmins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("sysId,sysname,sysgender,sysbirthday,syscId,syspassword")] SysAdmin sysAdmin)
        {
            if (id != sysAdmin.sysId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sysAdmin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SysAdminExists(sysAdmin.sysId))
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
            return View(sysAdmin);
        }

        // GET: SysAdmins/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sysAdmin = await _context.SysAdmin
                .FirstOrDefaultAsync(m => m.sysId == id);
            if (sysAdmin == null)
            {
                return NotFound();
            }

            return View(sysAdmin);
        }

        // POST: SysAdmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var sysAdmin = await _context.SysAdmin.FindAsync(id);
            if (sysAdmin != null)
            {
                _context.SysAdmin.Remove(sysAdmin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SysAdminExists(string id) => _context.SysAdmin.Any(e => e.sysId == id);
    }
}
