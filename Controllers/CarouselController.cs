using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class CarouselController : Controller
{

    public IActionResult Index()
    {
        // 图片文件夹路径
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lib", "SeedSourcePhoto");

        // 打印路径到调试输出
        System.Diagnostics.Debug.WriteLine($"Folder path: {folderPath}");

        // 确保路径存在
        if (!Directory.Exists(folderPath))
        {
            System.Diagnostics.Debug.WriteLine("Directory does not exist.");
            return View(new List<string>()); // 返回一个空列表
        }

        // 获取所有图片文件的相对路径
        var files = Directory.GetFiles(folderPath, "*.*")
                              .Where(file => file.EndsWith(".jpg") || file.EndsWith(".png"))
                              .Select(file => Path.GetFileName(file))
                              .ToList();

        // 打印文件名到调试输出
        foreach (var file in files)
        {
            System.Diagnostics.Debug.WriteLine($"File found: {file}");
        }

        return View(files); // 传递文件名列表到视图
    }
}

