using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgrochemicalAPI.Data;
using AgrochemicalAPI.Models;

namespace AgrochemicalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerrController : ControllerBase
    {
        private readonly AgrochemicalDbContext _context;

        public ManufacturerrController(AgrochemicalDbContext context)
        {
            _context = context;
        }

        // GET: api/Manufacturerr
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Manufacturerr>>> GetManufacturerr()
        {
            return await _context.Manufacturerr.ToListAsync();
        }

        // GET: api/Manufacturerr/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Manufacturerr>> GetManufacturerr(int id)
        {
            var manufacturerr = await _context.Manufacturerr.FindAsync(id);

            if (manufacturerr == null)
            {
                return NotFound();
            }

            return manufacturerr;
        }

        // PUT: api/Manufacturerr/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutManufacturerr(int id, Manufacturerr manufacturerr)
        {
            if (id != manufacturerr.ManufacturerrId)
            {
                return BadRequest();
            }

            _context.Entry(manufacturerr).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManufacturerrExists(id))
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

        // POST: api/Manufacturerr
        [HttpPost]
        public async Task<ActionResult<Manufacturerr>> PostManufacturerr(Manufacturerr manufacturerr)
        {
            _context.Manufacturerr.Add(manufacturerr);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetManufacturerr", new { id = manufacturerr.ManufacturerrId }, manufacturerr);
        }

        // DELETE: api/Manufacturerr/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Manufacturerr>> DeleteManufacturerr(int id)
        {
            var manufacturerr = await _context.Manufacturerr.FindAsync(id);
            if (manufacturerr == null)
            {
                return NotFound();
            }

            _context.Manufacturerr.Remove(manufacturerr);
            await _context.SaveChangesAsync();

            return manufacturerr;
        }

        private bool ManufacturerrExists(int id)
        {
            return _context.Manufacturerr.Any(e => e.ManufacturerrId == id);
        }
    }
}
