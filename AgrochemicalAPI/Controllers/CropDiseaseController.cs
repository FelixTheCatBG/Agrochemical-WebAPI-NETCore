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
    public class CropDiseaseController : ControllerBase
    {
        private readonly AgrochemicalDbContext _context;

        public CropDiseaseController(AgrochemicalDbContext context)
        {
            _context = context;
        }

        // GET: api/CropIllness
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CropDisease>>> GetCropIllnesses()
        {
            var cropIllness = await _context.CropDiseases
                .Where(ci => ci.Crop.CropCategory.Name == "Corns")
                .Select(ci => ci.Disease.DiseaseSymtpoms.Select(s => new
                    {
                        SympomList = s.Symptom.Name
                    })
                ).ToListAsync();

            return Ok(cropIllness);
        }

        // GET: api/CropIllness/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CropDisease>> GetCropIllness(int id)
        {
            var cropIllness = await _context.CropDiseases.FindAsync(id);

            if (cropIllness == null)
            {
                return NotFound();
            }

            return cropIllness;
        }

        // PUT: api/CropIllness/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCropIllness(int id, CropDisease cropIllness)
        {
            if (id != cropIllness.CropId)
            {
                return BadRequest();
            }

            _context.Entry(cropIllness).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CropIllnessExists(id))
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

        // POST: api/CropIllness
        [HttpPost]
        public async Task<ActionResult<CropDisease>> PostCropIllness(CropDisease cropIllness)
        {
            _context.CropDiseases.Add(cropIllness);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CropIllnessExists(cropIllness.CropId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCropIllness", new { id = cropIllness.CropId }, cropIllness);
        }

        // DELETE: api/CropIllness/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CropDisease>> DeleteCropIllness(int id)
        {
            var cropIllness = await _context.CropDiseases.FindAsync(id);
            if (cropIllness == null)
            {
                return NotFound();
            }

            _context.CropDiseases.Remove(cropIllness);
            await _context.SaveChangesAsync();

            return cropIllness;
        }

        private bool CropIllnessExists(int id)
        {
            return _context.CropDiseases.Any(e => e.CropId == id);
        }
    }
}
