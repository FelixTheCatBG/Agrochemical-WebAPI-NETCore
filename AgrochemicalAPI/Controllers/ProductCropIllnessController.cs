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
    public class ProductCropIllnessController : ControllerBase
    {
        private readonly AgrochemicalDbContext _context;

        public ProductCropIllnessController(AgrochemicalDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductCropIllness
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCropIllness>>> GetProductCropIllnesses()
        {
            return await _context.ProductCropIllnesses.ToListAsync();
        }

        // GET: api/ProductCropIllness/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCropIllness>> GetProductCropIllness(int id)
        {
            var productCropIllness = await _context.ProductCropIllnesses.FindAsync(id);

            if (productCropIllness == null)
            {
                return NotFound();
            }

            return productCropIllness;
        }

        // PUT: api/ProductCropIllness/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductCropIllness(int id, ProductCropIllness productCropIllness)
        {
            if (id != productCropIllness.ProductCropIllnessId)
            {
                return BadRequest();
            }

            _context.Entry(productCropIllness).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCropIllnessExists(id))
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

        // POST: api/ProductCropIllness
        [HttpPost]
        public async Task<ActionResult<ProductCropIllness>> PostProductCropIllness(ProductCropIllness productCropIllness)
        {
            _context.ProductCropIllnesses.Add(productCropIllness);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductCropIllness", new { id = productCropIllness.ProductCropIllnessId }, productCropIllness);
        }

        // DELETE: api/ProductCropIllness/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductCropIllness>> DeleteProductCropIllness(int id)
        {
            var productCropIllness = await _context.ProductCropIllnesses.FindAsync(id);
            if (productCropIllness == null)
            {
                return NotFound();
            }

            _context.ProductCropIllnesses.Remove(productCropIllness);
            await _context.SaveChangesAsync();

            return productCropIllness;
        }

        private bool ProductCropIllnessExists(int id)
        {
            return _context.ProductCropIllnesses.Any(e => e.ProductCropIllnessId == id);
        }
    }
}
