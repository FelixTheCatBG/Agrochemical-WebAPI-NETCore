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
    public class CropCategoryController : ControllerBase
    {
        private readonly AgrochemicalDbContext _context;

        public CropCategoryController(AgrochemicalDbContext context)
        {
            _context = context;
        }

        // GET: api/CropCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CropCategory>>> GetCropCategories()
        {
            return await _context.CropCategories.ToListAsync();
        }

        // GET: api/CropCategory/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CropCategory>> GetCropCategory(int id)
        {
            var cropCategory = await _context.CropCategories.FindAsync(id);

            if (cropCategory == null)
            {
                return NotFound();
            }

            return cropCategory;
        }

        // PUT: api/CropCategory/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCropCategory(int id, CropCategory cropCategory)
        {
            if (id != cropCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(cropCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CropCategoryExists(id))
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

        // POST: api/CropCategory
        [HttpPost]
        public async Task<ActionResult<CropCategory>> PostCropCategory(CropCategory cropCategory)
        {
            _context.CropCategories.Add(cropCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCropCategory", new { id = cropCategory.Id }, cropCategory);
        }

        // DELETE: api/CropCategory/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CropCategory>> DeleteCropCategory(int id)
        {
            var cropCategory = await _context.CropCategories.FindAsync(id);
            if (cropCategory == null)
            {
                return NotFound();
            }

            _context.CropCategories.Remove(cropCategory);
            await _context.SaveChangesAsync();

            return cropCategory;
        }

        private bool CropCategoryExists(int id)
        {
            return _context.CropCategories.Any(e => e.Id == id);
        }
    }
}
