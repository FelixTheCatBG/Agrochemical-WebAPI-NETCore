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
    public class ProductCategoryController : ControllerBase
    {
        private readonly AgrochemicalDbContext _context;

        public ProductCategoryController(AgrochemicalDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductCategory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetProductCategories()
        {
            return await _context.ProductCategories.ToListAsync();
        }

        // GET: api/ProductCategory/5
        [HttpGet("{id}")]
        public IActionResult GetProductCategory([FromRoute] int id)
        {
            var result = _context.ProductCategories
             .Where(p => p.Id == id)
             .Select(x => new
             {
                 ProductName = x.Name,
                 Products = x.Products.Select(pr => new
                 {
                     Id = pr.Id,
                     Name = pr.Name,
                     Manufacturer = pr.Manufacturerr.Name,
                     Description = pr.Description
                 }).ToList()
             }).SingleOrDefault(); 

            return Ok(result);
        }

        // PUT: api/ProductCategory/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductCategory(int id, ProductCategory productCategory)
        {
            if (id != productCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(productCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductCategoryExists(id))
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

        // POST: api/ProductCategory
        [HttpPost]
        public async Task<ActionResult<ProductCategory>> PostProductCategory(ProductCategory productCategory)
        {
            _context.ProductCategories.Add(productCategory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProductCategory", new { id = productCategory.Id }, productCategory);
        }

        // DELETE: api/ProductCategory/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductCategory>> DeleteProductCategory(int id)
        {
            var productCategory = await _context.ProductCategories.FindAsync(id);
            if (productCategory == null)
            {
                return NotFound();
            }

            _context.ProductCategories.Remove(productCategory);
            await _context.SaveChangesAsync();

            return productCategory;
        }

        private bool ProductCategoryExists(int id)
        {
            return _context.ProductCategories.Any(e => e.Id == id);
        }
    }
}
