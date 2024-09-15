using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using D05Project.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Newtonsoft.Json;

namespace D05Project.Controllers
{
    public class AdminsController : Controller
    {
        private readonly ProjectDContext _context;

        public AdminsController(ProjectDContext context)
        {
            _context = context;
        }

        // GET: Admins
        public async Task<IActionResult> Index()
        {
            var projectDContext = _context.Admin.Include(a => a.sys);
            return View(await projectDContext.ToListAsync());
        }

        // GET: Admins/Details/5
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

        // GET: Admins/Create
        public IActionResult Create()
        {
            ViewData["sysId"] = new SelectList(_context.SysAdmin, "sysId", "sysId");
            return View();
        }

        // POST: Admins/Create
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

        // GET: Admins/Edit/5
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

        // POST: Admins/Edit/5
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

        // GET: Admins/Delete/5
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

        // POST: Admins/Delete/5
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

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string aId, string Password)
        {
            if (aId == null || Password == null)
            {
                return View();
            }
            var result = await _context.Admin.Where(m => m.aId == aId && m.Password == Password).FirstOrDefaultAsync();
            if (result == null)
            {
                ViewData["ErrorMessage"] = "帳號密碼錯誤";
                return View(aId);

            }

            //HttpContext.Session.SetString("Manager", JsonConvert.SerializeObject(aId));
            //return RedirectToAction("Index", "Admins" );
            return RedirectToAction("Details", new { id = result.aId });
        }

        private bool AdminExists(string id) => _context.Admin.Any(e => e.aId == id);
    }
}
