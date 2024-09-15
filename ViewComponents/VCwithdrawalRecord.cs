using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using D05Project.Models;

namespace D05Project.ViewComponents
{
    public class VCwithdrawalRecord : ViewComponent
    {

        private readonly ProjectDContext _context;

        public VCwithdrawalRecord(ProjectDContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var wr = _context.withdrawalRecord.Where(p => p.pNo == id).Include(p => p.pIdNavigation).Include(p => p.sIdNavigation); ;
           

            return View(await wr.ToListAsync());
        }



    }
}
