using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using ODataExample.Data;
using ODataExample.Models;

namespace ODataExample.Controllers
{
    public class IlcelerController : ODataController
    {
        private readonly AppDbContext _context;

        public IlcelerController(AppDbContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<Ilce> Get()
        {
            return _context.Ilceler.Include(i => i.Sehir).ThenInclude(s => s.Ulke);
        }

        [EnableQuery]
        public async Task<IActionResult> Get(int key)
        {
            var ilce = await _context.Ilceler
                .Include(i => i.Sehir)
                .ThenInclude(s => s.Ulke)
                .FirstOrDefaultAsync(i => i.Id == key);

            if (ilce == null)
            {
                return NotFound();
            }

            return Ok(ilce);
        }

        public async Task<IActionResult> Post([FromBody] Ilce ilce)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Ilceler.Add(ilce);
            await _context.SaveChangesAsync();

            return Created(ilce);
        }

        public async Task<IActionResult> Put(int key, [FromBody] Ilce ilce)
        {
            if (key != ilce.Id)
            {
                return BadRequest();
            }

            _context.Entry(ilce).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IlceExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(ilce);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var ilce = await _context.Ilceler.FindAsync(key);
            if (ilce == null)
            {
                return NotFound();
            }

            _context.Ilceler.Remove(ilce);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IlceExists(int id)
        {
            return _context.Ilceler.Any(e => e.Id == id);
        }
    }
}
