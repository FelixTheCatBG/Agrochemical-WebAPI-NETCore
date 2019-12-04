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
    public class IllnessSymptomController : ControllerBase
    {
        private readonly AgrochemicalDbContext _context;

        public IllnessSymptomController(AgrochemicalDbContext context)
        {
            _context = context;
        }

        // GET: api/IllnessSymptom
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IllnessSymptom>>> GetIllnessSymptoms()
        {
            return await _context.IllnessSymptoms.ToListAsync();
        }

        // GET: api/IllnessSymptom/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IllnessSymptom>> GetIllnessSymptom(int id)
        {
            var illnessSymptom = await _context.IllnessSymptoms.FindAsync(id);

            if (illnessSymptom == null)
            {
                return NotFound();
            }

            return illnessSymptom;
        }

        // PUT: api/IllnessSymptom/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIllnessSymptom(int id, IllnessSymptom illnessSymptom)
        {
            if (id != illnessSymptom.IllnessId)
            {
                return BadRequest();
            }

            _context.Entry(illnessSymptom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IllnessSymptomExists(id))
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

        // POST: api/IllnessSymptom
        [HttpPost]
        public async Task<ActionResult<IllnessSymptom>> PostIllnessSymptom(IllnessSymptom illnessSymptom)
        {
            _context.IllnessSymptoms.Add(illnessSymptom);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (IllnessSymptomExists(illnessSymptom.IllnessId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetIllnessSymptom", new { id = illnessSymptom.IllnessId }, illnessSymptom);
        }

        // DELETE: api/IllnessSymptom/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<IllnessSymptom>> DeleteIllnessSymptom(int id)
        {
            var illnessSymptom = await _context.IllnessSymptoms.FindAsync(id);
            if (illnessSymptom == null)
            {
                return NotFound();
            }

            _context.IllnessSymptoms.Remove(illnessSymptom);
            await _context.SaveChangesAsync();

            return illnessSymptom;
        }

        private bool IllnessSymptomExists(int id)
        {
            return _context.IllnessSymptoms.Any(e => e.IllnessId == id);
        }
    }
}
