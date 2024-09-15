using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using D05Project.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace D05Project.Controllers
{
    public class MainPageController : Controller
    {
        private readonly ProjectDContext _context;
        private readonly HttpClient _httpClient;
        public MainPageController(ProjectDContext context)
        {
            _context = context;
        }

        // GET: MainPage


        // GET: MainPage/Create
        public IActionResult Create() => View();

        // POST: MainPage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("MemId,MemName,MemTel,MemEmail,MemBirth,Memaddress,Memgender")] Member member)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(member);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(member);
        //}
        
        // GET: MainPage/Edit/5


        public IActionResult ExceptionTest()
        {
            int a = 0;
            int s = 100 / a;
            return View();
        }


        public IActionResult Index()
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lib", "SeedSourcePhoto");

            if (!Directory.Exists(folderPath))
            {
                // 处理文件夹不存在的情况
                return View(new List<string>());
            }

            var files = Directory.GetFiles(folderPath, "*.*")
                                  .Where(file => file.EndsWith(".jpg") || file.EndsWith(".png"))
                                  .Select(file => Path.GetFileName(file))
                                  .ToList();

            return View(files);
        }



        //-------------------
        // Fetch weather data from API
        private async Task<object> FetchWeatherDataAsync()
        {
            string apiKey = "CWA-F689F2CB-B051-48FE-BF21-46C76136FEF5";
            string url = $"https://opendata.cwb.gov.tw/api/v1/rest/datastore/F-C0032-001?Authorization={apiKey}";

            try
            {
                var response = await _httpClient.GetStringAsync(url);
                var weatherData = JObject.Parse(response);

                var location = weatherData["records"]["location"][0];
                var locationName = location["locationName"].ToString();
                var weatherElement = location["weatherElement"];
                var description = weatherElement[0]["time"][0]["parameter"]["parameterName"].ToString();
                var maxTemp = weatherElement[4]["time"][0]["parameter"]["parameterName"].ToString();
                var minTemp = weatherElement[2]["time"][0]["parameter"]["parameterName"].ToString();

                var result = new
                {
                    LocationName = locationName,
                    Description = description,
                    MaxTemp = maxTemp,
                    MinTemp = minTemp
                };

                return Json(result);
            }
            catch (HttpRequestException ex)
            {
                return Json(new { Error = $"Error retrieving weather data: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return Json(new { Error = $"Unexpected error: {ex.Message}" });
            }
        }
    }

    // GET: MainPage/Create
    //public IActionResult Create() => View();

    //// POST: MainPage/Create
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Create([Bind("MemId,MemName,MemTel,MemEmail,MemBirth,Memaddress,Memgender")] Member member)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        _context.Add(member);
    //        await _context.SaveChangesAsync();
    //        return RedirectToAction(nameof(Index));
    //    }
    //    return View(member);
    //}

    // GET: MainPage/ExceptionTest
    //public IActionResult ExceptionTest()
    //{
    //    try
    //    {
    //        int a = 0;
    //        int s = 100 / a; // This will throw a divide-by-zero exception
    //    }
    //    catch (Exception ex)
    //    {
    //        ViewData["Error"] = $"Exception occurred: {ex.Message}";
    //    }
    //    return View();
    //}
}


        //------------------
//    }
//}
