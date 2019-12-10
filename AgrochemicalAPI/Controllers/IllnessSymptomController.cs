using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgrochemicalAPI.Data;
using AgrochemicalAPI.Models;
using Newtonsoft.Json.Linq;

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
            return await _context.IllnessSymptoms.Include(ilnes => ilnes.Symptom).Include(ilnes => ilnes.Illness).ToListAsync();
        }

        // GET: api/IllnessSymptom/5
        [HttpGet("{id}")]
        public  IActionResult GetIllnessSymptom()
        {
            //var listofSymptoms = new List<int>();
            //listofSymptoms.AddRange(array);
            //list1.All(x => list2.Any(y => x.SupplierId == y.SupplierId));

            //var illnesses = _context.Illnesses
            //    .Include(iln => iln.IllnessSymptoms)
            //    .Where(ilnes => ilnes.IllnessSymptoms.All(l => searchSymptoms.Contains(l.SymptomId)))
            //    .Select(e => e.Name)
            //    .ToList();

            //This works only if all the illness symptoms are containet in the search symptoms
            //var illnesses = _context.Illnesses
            //    .Include(iln => iln.IllnessSymptoms)
            //    //.ThenInclude(ilna => ilna.Symptom)
            //    .Where(ilnes => ilnes.IllnessSymptoms.All(l => searchSymptoms.Any(y => l.SymptomId == y)))
            //    .Select(e => e.Name)
            //    .ToList();

            //This works only if all  search symptoms are containet in the the illness symptoms 
            //var illnesses = _context.Illnesses
            //    //.Include(ilnes => ilnes.IllnessSymptoms)
            //    //.ThenInclude(ilna => ilna.Symptom)
            //    .Where(ilnes => searchSymptoms.All(l => ilnes.IllnessSymptoms.Any(y => l == y.SymptomId)))
            //    .Select(e => e.Name)
            //    .ToList();

            var searchSymptoms = new List<int> { 2 };
            var searchSymptoms2 = new List<int> { 1, 3, 4, 5, 6, 7, 8, 9 };

            var illnesses = _context.Illnesses
                .Where(i => searchSymptoms.All(ss => i.IllnessSymptoms.Any(ils => ss == ils.SymptomId)))
                .Select(i => i.Name)
                .ToList();

            if (illnesses == null)
            {
                return NotFound();
            }
           
            var boolleana = searchSymptoms.All(x => searchSymptoms2.Any(y => x == y));

            return Ok(illnesses);
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
