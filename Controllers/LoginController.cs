using D05Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;

namespace D05Project.Controllers
{
    public class LoginController : Controller
    {

        private readonly ProjectDContext _context;

        public LoginController(ProjectDContext context)
        {
            _context = context;
        }

        //4.2.4 建立Get與Post的Login Action
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
           var result =await _context.Admin.Where(m => m.aId == aId && m.Password == Password).FirstOrDefaultAsync();
                if (result == null)
            {
                ViewData["ErrorMessage"] = "帳號密碼錯誤";
                return View(aId);

            }
            //return RedirectToAction("Index", "Admins" );
            return RedirectToAction("Details", new { id = result.aId});
        }

        
        ////如果帳密正確,導入後台頁面

        ////如果帳密不正確,回到登入頁面,並告知帳密錯誤

        ////如果帳密正確
        ////  導入後台頁面
        ////否則
        //// 回到登入頁面,並告知帳密錯誤

        //if (login == null)
        //{
        //    return View();
        //}


        //select * from Login where account=@account and password=@password

        //    var result = await _context.Admin.Where(m => m.aId == Admin.aId && m.Password == Admin.Password).FirstOrDefaultAsync();


        //    if (result == null)
        //    {
        //        //4.2.6 將ViewData["Error"]加入Login View
        //        ViewData["Error"] = "帳號或密碼錯誤!!";
        //        return View();
        //    }
        //    else
        //    {
        //        //Session["Manager"] = "已登入";

        //        HttpContext.Session.SetString("Manager", JsonConvert.SerializeObject(login));

        //        return RedirectToAction("Index", "Books");
        //    }



        //}

        //public IActionResult Logout()
        //{


        //    HttpContext.Session.Remove("Manager");
        //    return RedirectToAction("Index", "Home");

        //    //return View();
        //}
        //}
        //}

    }
}