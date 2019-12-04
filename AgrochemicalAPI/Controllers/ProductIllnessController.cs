﻿using System;
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
    public class ProductIllnessController : ControllerBase
    {
        private readonly AgrochemicalDbContext _context;

        public ProductIllnessController(AgrochemicalDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductIllness
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductIllness>>> GetProductIllnesses()
        {
            return await _context.ProductIllnesses.ToListAsync();
        }

        // GET: api/ProductIllness/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductIllness>> GetProductIllness(int id)
        {
            var productIllness = await _context.ProductIllnesses.FindAsync(id);

            if (productIllness == null)
            {
                return NotFound();
            }

            return productIllness;
        }

        // PUT: api/ProductIllness/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductIllness(int id, ProductIllness productIllness)
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
        public async Task<ActionResult<ProductIllness>> PostProductIllness(ProductIllness productIllness)
        {
            _context.ProductIllnesses.Add(productIllness);
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
        public async Task<ActionResult<ProductIllness>> DeleteProductIllness(int id)
        {
            var productIllness = await _context.ProductIllnesses.FindAsync(id);
            if (productIllness == null)
            {
                return NotFound();
            }

            _context.ProductIllnesses.Remove(productIllness);
            await _context.SaveChangesAsync();

            return productIllness;
        }

        private bool ProductIllnessExists(int id)
        {
            return _context.ProductIllnesses.Any(e => e.ProductId == id);
        }
    }
}