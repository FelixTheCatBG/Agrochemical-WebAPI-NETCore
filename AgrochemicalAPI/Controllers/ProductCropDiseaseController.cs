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
    public class ProductCropDiseaseController : ControllerBase
    {
        private readonly AgrochemicalDbContext _context;

        public ProductCropDiseaseController(AgrochemicalDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductCropIllness
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCropDisease>>> GetProductCropIllnesses()
        {
            return await _context.ProductCropDiseases.ToListAsync();
        }

        // GET: api/ProductCropIllness/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCropDisease>> GetProductCropIllness(int id)
        {
            var productCropIllness = await _context.ProductCropDiseases.FindAsync(id);

            if (productCropIllness == null)
            {
                return NotFound();
            }

            return productCropIllness;
        }

        // PUT: api/ProductCropIllness/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductCropIllness(int id, ProductCropDisease productCropIllness)
        {
            if (id != productCropIllness.ProductCropDiseaseId)
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
        public async Task<ActionResult<ProductCropDisease>> PostProductCropIllness(ProductCropDisease productCropIllness)
        {
            _context.ProductCropDiseases.Add(productCropIllness);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductCropIllness", new { id = productCropIllness.ProductCropDiseaseId }, productCropIllness);
        }

        // DELETE: api/ProductCropIllness/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductCropDisease>> DeleteProductCropIllness(int id)
        {
            var productCropIllness = await _context.ProductCropDiseases.FindAsync(id);
            if (productCropIllness == null)
            {
                return NotFound();
            }

            _context.ProductCropDiseases.Remove(productCropIllness);
            await _context.SaveChangesAsync();

            return productCropIllness;
        }

        private bool ProductCropIllnessExists(int id)
        {
            return _context.ProductCropDiseases.Any(e => e.ProductCropDiseaseId == id);
        }
    }
}
