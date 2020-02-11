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
using Microsoft.AspNetCore.Authorization;

namespace AgrochemicalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AgrochemicalDbContext _context;

        public ProductController(AgrochemicalDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        // GET: api/Product
        [HttpGet]
        public  IActionResult GetProducts()
        {
           var productsList =  _context.Products
                .Select(p => new
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Category = p.ProductCategory.Name,
                    Manufacturer = p.Manufacturerr.Name,
                }).ToList();

            return Ok(productsList);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetProduct([FromRoute] int id)
        {
            var result = _context.Products
                .Where(p => p.Id == id)
                 .Select(p => new
                 {
                     ProductId = p.Id,
                     ProductName= p.Name,
                     ActiveIngredient = p.ActiveIngredient,
                     Description = p.Description,
                     Mechanism = p.MechanismOfAction,
                     HowToRecommendation = p.HowToUseRecommendation,
                     Category = p.ProductCategory.Name,
                     ProductAdvantages = p.ProductAdvantages.Select(pr => new 
                     { 
                     Advantage = pr.Advantage
                     }).ToList(),
                     Manufacturer = p.Manufacturerr.Name,
                     ProductCropDisease = p.ProductCropDiseases.Select(pr => new
                     {
                         Crop = pr.Crop.Name,
                         Disease = pr.Disease.Name,
                         Dosage = pr.Dosage,
                         Period = pr.Period
                     }).ToList(),
                     Packages = p.Packages.Select(pa => new
                     {
                         PackageAmount = pa.Amount,
                         PackageUnit = pa.Unit,
                         PackagePrice = pa.Price
                     }).ToList(),
                 }).SingleOrDefault();

            if (result == null)
            {
                return NotFound();
            }
              
            return Ok(result);
        }


        [Authorize(Roles = "Admin")]
        // PUT: api/Product/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        [Authorize(Roles="Admin")]
        [HttpPost]
        public async Task<ActionResult> PostProduct([FromBody]Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }


        [Authorize(Roles = "Admin")]
        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return product;
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
