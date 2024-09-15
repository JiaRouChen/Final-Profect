using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using D05Project.Models;

namespace D05Project.ViewComponents
{
    public class VCOrderDetails:ViewComponent
    {

        private readonly ProjectDContext _context;

        public VCOrderDetails(ProjectDContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var od = _context.OrderDetail.Where(o => o.orderId == id).Include(o => o.pIdNavigation);
           

            return View(await od.ToListAsync());
        }



    }
}
