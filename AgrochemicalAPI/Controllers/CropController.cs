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
    public class CropController : ControllerBase
    {
        private readonly AgrochemicalDbContext _context;

        public CropController(AgrochemicalDbContext context)
        {
            _context = context;
        }

        // GET: api/Crop
        [HttpGet]
        public IActionResult GetCrops()
        {
          var result =  _context.Crops
            //.Where(cr => cr.CropCategory.Name =="Corn")
            .Select(p => new
            {
                CropName = p.Name,
                CropIllnesses = p.CropIllnesses.Select(cp => new
                {
                    Id = cp.CropId,
                    Name = cp.Crop.Name,
                    Symtpoms = cp.Illness.IllnessSymptoms.Select(ils => new { ils.Symptom.Name }).ToList()
                }).ToList()
            }).ToList();

            return Ok(result);
        }

        // GET: api/Crop/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Crop>> GetCrop(int id)
        {
            var crop = await _context.Crops.FindAsync(id);

            if (crop == null)
            {
                return NotFound();
            }

            return crop;
        }

        // PUT: api/Crop/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCrop(int id, Crop crop)
        {
            if (id != crop.Id)
            {
                return BadRequest();
            }

            _context.Entry(crop).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CropExists(id))
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

        // POST: api/Crop
        [HttpPost]
        public async Task<ActionResult<Crop>> PostCrop(Crop crop)
        {
            _context.Crops.Add(crop);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCrop", new { id = crop.Id }, crop);
        }

        // DELETE: api/Crop/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Crop>> DeleteCrop(int id)
        {
            var crop = await _context.Crops.FindAsync(id);
            if (crop == null)
            {
                return NotFound();
            }

            _context.Crops.Remove(crop);
            await _context.SaveChangesAsync();

            return crop;
        }

        private bool CropExists(int id)
        {
            return _context.Crops.Any(e => e.Id == id);
        }
    }
}
