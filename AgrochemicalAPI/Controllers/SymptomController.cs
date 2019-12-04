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
    public class SymptomController : ControllerBase
    {
        private readonly AgrochemicalDbContext _context;

        public SymptomController(AgrochemicalDbContext context)
        {
            _context = context;
        }

        // GET: api/Symptom
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Symptom>>> GetSymptoms()
        {
            return await _context.Symptoms.ToListAsync();
        }

        // GET: api/Symptom/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Symptom>> GetSymptom(int id)
        {
            var symptom = await _context.Symptoms.FindAsync(id);

            if (symptom == null)
            {
                return NotFound();
            }

            return symptom;
        }

        // PUT: api/Symptom/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSymptom(int id, Symptom symptom)
        {
            if (id != symptom.Id)
            {
                return BadRequest();
            }

            _context.Entry(symptom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SymptomExists(id))
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

        // POST: api/Symptom
        [HttpPost]
        public async Task<ActionResult<Symptom>> PostSymptom(Symptom symptom)
        {
            _context.Symptoms.Add(symptom);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSymptom", new { id = symptom.Id }, symptom);
        }

        // DELETE: api/Symptom/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Symptom>> DeleteSymptom(int id)
        {
            var symptom = await _context.Symptoms.FindAsync(id);
            if (symptom == null)
            {
                return NotFound();
            }

            _context.Symptoms.Remove(symptom);
            await _context.SaveChangesAsync();

            return symptom;
        }

        private bool SymptomExists(int id)
        {
            return _context.Symptoms.Any(e => e.Id == id);
        }
    }
}
