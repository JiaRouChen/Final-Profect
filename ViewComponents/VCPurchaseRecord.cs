using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using D05Project.Models;

namespace D05Project.ViewComponents
{
    public class VCPurchaseRecord : ViewComponent
    {

        private readonly ProjectDContext _context;

        public VCPurchaseRecord(ProjectDContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var pr = _context.PurchaseRecord.Where(p => p.sNo == id).Include(p => p.pIdNavigation).Include(p => p.sIdNavigation);
           

            return View(await pr.ToListAsync());
        }



    }
}
