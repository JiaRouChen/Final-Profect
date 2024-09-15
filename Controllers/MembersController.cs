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
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace D05Project.Controllers
{
    public class MembersController : Controller
    {
        private readonly ProjectDContext _context;

        public MembersController(ProjectDContext context)
        {
            _context = context;
        }

        // GET: Members
        public async Task<IActionResult> Index() => View(await _context.Member.ToListAsync());

        // GET: Members/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .FirstOrDefaultAsync(m => m.MemId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create() => View();

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemId,MemName,MemTel,MemEmail,MemBirth,Memaddress,Memgender")] Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MemId,MemName,MemTel,MemEmail,MemBirth,Memaddress,Memgender")] Member member)
        {
            if (id != member.MemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.MemId))
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
            return View(member);
        }

        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Member
                .FirstOrDefaultAsync(m => m.MemId == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var member = await _context.Member.FindAsync(id);
            if (member != null)
            {
                _context.Member.Remove(member);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        //public IActionResult Login()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Login(string Memaddress, string MemName)
        //{
        //    if (Memaddress == null || MemName == null)
        //    {
        //        return View();
        //    }
        //    var result = await _context.Member.Where(x => x.Memaddress == Memaddress && x.MemName == MemName).FirstOrDefaultAsync();
        //    if (result == null)
        //    {
        //        ViewData["ErrorMessage"] = "帳號密碼錯誤";
        //        return View(Memaddress);

        //    }
        //    //return RedirectToAction("Index", "Admins" );
        //    return RedirectToAction("Details", new { id = result.MemId });
        //}



        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string MemEmail, int MemTel)
        {
            if (MemEmail == null || MemTel == null)
            {
                return View();
            }
            var result = await _context.Member.Where(x => x.MemEmail == MemEmail && x.MemTel == MemTel).FirstOrDefaultAsync();
            if (result == null)
            {
                ViewData["ErrorMessage"] = "帳號密碼錯誤";
                return View(MemEmail);

            }
            //return RedirectToAction("Index", "Admins" );
            HttpContext.Session.SetString("Manager", JsonConvert.SerializeObject(MemEmail));
            
            return RedirectToAction("Details", new { id = result.MemId });
        }

        //
        public IActionResult ChangePassword()
        {
            return View();
        }

        // POST: Member/ChangePassword
        
        //



        private bool MemberExists(string id) => _context.Member.Any(e => e.MemId == id);
    }
}
