using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;
using ODataExample.Data;
using ODataExample.Models;

namespace ODataExample.Controllers
{
    public class UlkelerController : ODataController
    {
        private readonly AppDbContext _context;

        public UlkelerController(AppDbContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IQueryable<Ulke> Get()
        {
            return _context.Ulkeler.Include(u => u.Sehirler);
        }

        [EnableQuery]
        public async Task<IActionResult> Get(int key)
        {
            var ulke = await _context.Ulkeler
                .Include(u => u.Sehirler)
                .FirstOrDefaultAsync(u => u.Id == key);

            if (ulke == null)
            {
                return NotFound();
            }

            return Ok(ulke);
        }

        public async Task<IActionResult> Post([FromBody] Ulke ulke)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Ulkeler.Add(ulke);
            await _context.SaveChangesAsync();

            return Created(ulke);
        }

        public async Task<IActionResult> Put(int key, [FromBody] Ulke ulke)
        {
            if (key != ulke.Id)
            {
                return BadRequest();
            }

            _context.Entry(ulke).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UlkeExists(key))
                {
                    return NotFound();
                }
                throw;
            }

            return Updated(ulke);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var ulke = await _context.Ulkeler.FindAsync(key);
            if (ulke == null)
            {
                return NotFound();
            }

            _context.Ulkeler.Remove(ulke);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UlkeExists(int id)
        {
            return _context.Ulkeler.Any(e => e.Id == id);
        }
    }
}
