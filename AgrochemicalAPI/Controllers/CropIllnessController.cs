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
    public class CropIllnessController : ControllerBase
    {
        private readonly AgrochemicalDbContext _context;

        public CropIllnessController(AgrochemicalDbContext context)
        {
            _context = context;
        }

        // GET: api/CropIllness
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CropIllness>>> GetCropIllnesses()
        {
            return await _context.CropIllnesses.ToListAsync();
        }

        // GET: api/CropIllness/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CropIllness>> GetCropIllness(int id)
        {
            var cropIllness = await _context.CropIllnesses.FindAsync(id);

            if (cropIllness == null)
            {
                return NotFound();
            }

            return cropIllness;
        }

        // PUT: api/CropIllness/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCropIllness(int id, CropIllness cropIllness)
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
        public async Task<ActionResult<CropIllness>> PostCropIllness(CropIllness cropIllness)
        {
            _context.CropIllnesses.Add(cropIllness);
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
        public async Task<ActionResult<CropIllness>> DeleteCropIllness(int id)
        {
            var cropIllness = await _context.CropIllnesses.FindAsync(id);
            if (cropIllness == null)
            {
                return NotFound();
            }

            _context.CropIllnesses.Remove(cropIllness);
            await _context.SaveChangesAsync();

            return cropIllness;
        }

        private bool CropIllnessExists(int id)
        {
            return _context.CropIllnesses.Any(e => e.CropId == id);
        }
    }
}
