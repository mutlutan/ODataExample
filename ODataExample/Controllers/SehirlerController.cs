using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using ODataExample.Data;
using ODataExample.Models;

namespace ODataExample.Controllers
{
    public class SehirlerController : ODataController
    {
        private readonly AppDbContext _context;

        public SehirlerController(AppDbContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<Sehir> Get()
        {
            return _context.Sehirler.Include(s => s.Ulke).Include(s => s.Ilceler);
        }

        [EnableQuery]
        public async Task<IActionResult> Get(int key)
        {
            var sehir = await _context.Sehirler
                .Include(s => s.Ulke)
                .Include(s => s.Ilceler)
                .FirstOrDefaultAsync(s => s.Id == key);

            if (sehir == null)
            {
                return NotFound();
            }

            return Ok(sehir);
        }

        public async Task<IActionResult> Post([FromBody] Sehir sehir)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Sehirler.Add(sehir);
            await _context.SaveChangesAsync();

            return Created(sehir);
        }

        public async Task<IActionResult> Put(int key, [FromBody] Sehir sehir)
        {
            if (key != sehir.Id)
            {
                return BadRequest();
            }

            _context.Entry(sehir).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SehirExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(sehir);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var sehir = await _context.Sehirler.FindAsync(key);
            if (sehir == null)
            {
                return NotFound();
            }

            _context.Sehirler.Remove(sehir);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SehirExists(int id)
        {
            return _context.Sehirler.Any(e => e.Id == id);
        }
    }
}
