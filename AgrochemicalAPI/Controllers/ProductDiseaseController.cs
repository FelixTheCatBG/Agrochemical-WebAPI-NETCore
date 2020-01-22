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
    public class ProductDiseaseController : ControllerBase
    {
        private readonly AgrochemicalDbContext _context;

        public ProductDiseaseController(AgrochemicalDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductIllness
        [HttpGet]
        public IActionResult GetProductIllnesses([FromQuery] string illness)
        {
          
                var result = _context.ProductDiseases
                    .Where(p => p.Disease.Name == illness)
                     .Select(p => new
                     {
                         ProductId = p.Product.Id,
                         ProductName = p.Product.Name,
                         Category = p.Product.ProductCategory.Name,
                         Illnesses = p.Product.ProductDiseases.Select(pi => new
                         {
                             IllnessId = pi.Disease.Id,
                             IllnessName = pi.Disease.Name
                         }).ToList(),
                         Packages = p.Product.Packages.Select(pa => new
                         {
                             PackageAmount = pa.Amount,
                             PackageUnit = pa.Unit,
                             PackagePrice = pa.Price
                         }).ToList()
                     }).ToList();
                
                if (result == null)
                {
                    return NotFound();
                }

                return Ok(result);
            }

        // GET: api/ProductIllness/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDisease>> GetProductIllness(int id)
        {
            var productIllness = await _context.ProductDiseases.FindAsync(id);

            if (productIllness == null)
            {
                return NotFound();
            }

            return productIllness;
        }

        // PUT: api/ProductIllness/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductIllness(int id, ProductDisease productIllness)
        {
            if (id != productIllness.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(productIllness).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductIllnessExists(id))
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

        // POST: api/ProductIllness
        [HttpPost]
        public async Task<ActionResult<ProductDisease>> PostProductIllness(ProductDisease productIllness)
        {
            _context.ProductDiseases.Add(productIllness);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductIllnessExists(productIllness.ProductId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetProductIllness", new { id = productIllness.ProductId }, productIllness);
        }

        // DELETE: api/ProductIllness/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDisease>> DeleteProductIllness(int id)
        {
            var productIllness = await _context.ProductDiseases.FindAsync(id);
            if (productIllness == null)
            {
                return NotFound();
            }

            _context.ProductDiseases.Remove(productIllness);
            await _context.SaveChangesAsync();

            return productIllness;
        }

        private bool ProductIllnessExists(int id)
        {
            return _context.ProductDiseases.Any(e => e.ProductId == id);
        }
    }
}
