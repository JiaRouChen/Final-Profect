using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using D05Project.Models;

namespace D05Project.ViewComponents
{
    public class VCReBooks : ViewComponent
    {
        private readonly ProjectDContext _context;

        public VCReBooks(ProjectDContext  context)
        {
            _context = context;
        }
    

        //2.4.4 撰寫InvokeAsync()方法取得回覆留言資料
        public async Task<IViewComponentResult> InvokeAsync(long gid)
        {
            var rebook = await _context.ReBook.Where(m => m.GId == gid).OrderByDescending(m => m.TimeStamp).ThenByDescending(m => m.RId).ToListAsync();

            return View(rebook);
        }
    }
}

