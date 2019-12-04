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
    public class CropProductController : ControllerBase
    {
        private readonly AgrochemicalDbContext _context;

        public CropProductController(AgrochemicalDbContext context)
        {
            _context = context;
        }

        // GET: api/CropProduct
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CropProduct>>> GetCropProducts()
        {
            return await _context.CropProducts.ToListAsync();
        }

        // GET: api/CropProduct/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CropProduct>> GetCropProduct(int id)
        {
            var cropProduct = await _context.CropProducts.FindAsync(id);

            if (cropProduct == null)
            {
                return NotFound();
            }

            return cropProduct;
        }

        // PUT: api/CropProduct/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCropProduct(int id, CropProduct cropProduct)
        {
            if (id != cropProduct.CropId)
            {
                return BadRequest();
            }

            _context.Entry(cropProduct).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CropProductExists(id))
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

        // POST: api/CropProduct
        [HttpPost]
        public async Task<ActionResult<CropProduct>> PostCropProduct(CropProduct cropProduct)
        {
            _context.CropProducts.Add(cropProduct);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CropProductExists(cropProduct.CropId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCropProduct", new { id = cropProduct.CropId }, cropProduct);
        }

        // DELETE: api/CropProduct/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CropProduct>> DeleteCropProduct(int id)
        {
            var cropProduct = await _context.CropProducts.FindAsync(id);
            if (cropProduct == null)
            {
                return NotFound();
            }

            _context.CropProducts.Remove(cropProduct);
            await _context.SaveChangesAsync();

            return cropProduct;
        }

        private bool CropProductExists(int id)
        {
            return _context.CropProducts.Any(e => e.CropId == id);
        }
    }
}
