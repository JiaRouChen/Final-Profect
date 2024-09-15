using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;

namespace ProductAPI.Controllers
{
    [Route("apiProducts")]
    [ApiController]
    public class sProductsController : ControllerBase
    {
        private readonly dbProductsContext _context;

        public sProductsController(dbProductsContext context)
        {
            _context = context;
        }

        // GET: api/sProducts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<sProduct>>> GetsProduct()
        {
            return await _context.sProduct.ToListAsync();
        }

        // GET: api/sProducts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<sProduct>> GetsProduct(string id)
        {
            var sProduct = await _context.sProduct.FindAsync(id);

            if (sProduct == null)
            {
                return NotFound();
            }

            return sProduct;
        }

        // PUT: api/sProducts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutsProduct(string id, sProduct sProduct)
        {
            if (id != sProduct.PId)
            {
                return BadRequest();
            }

            _context.Entry(sProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!sProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/sProducts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<sProduct>> PostsProduct(sProduct sProduct)
        {
            _context.sProduct.Add(sProduct);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (sProductExists(sProduct.PId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetsProduct", new { id = sProduct.PId }, sProduct);
        }

        // DELETE: api/sProducts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletesProduct(string id)
        {
            var sProduct = await _context.sProduct.FindAsync(id);
            if (sProduct == null)
            {
                return NotFound();
            }

            _context.sProduct.Remove(sProduct);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool sProductExists(string id)
        {
            return _context.sProduct.Any(e => e.PId == id);
        }
    }
}
