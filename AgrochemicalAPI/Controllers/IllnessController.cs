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
    public class IllnessController : ControllerBase
    {
        private readonly AgrochemicalDbContext _context;

        public IllnessController(AgrochemicalDbContext context)
        {
            _context = context;
        }

        // GET: api/Illness
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Illness>>> GetIllnesses()
        {
            var result = _context.Illnesses                   
                      .Select(i => new
                      {
                          ProductId = i.Id,
                          ProductName = i.Name,
                          Description = i.Description,
                          //Products = i.ProductIllnesses.Select(pi => new
                          //{
                          //    ProductId = pi.Product.Id,
                          //    IllnessName = pi.Product.Name
                          //}).ToList()
                          Symptoms = i.IllnessSymptoms.Select(ils => new
                          {
                             IllnessSymptoms = ils.Symptom.Name
                          }).ToList(),
                      }).ToList();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/Illness/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Illness>> GetIllness(int id)
        {
            var illness = _context.Illnesses
                .Where(i => i.Id == id)
                .Select(i => new
                {
                    ProductId = i.Id,
                    ProductName = i.Name,
                    Description = i.Description,
                    //Products = i.ProductIllnesses.Select(pi => new
                    //{
                    //    ProductId = pi.Product.Id,
                    //    IllnessName = pi.Product.Name
                    //}).ToList()
                    Symptoms = i.IllnessSymptoms.Select(ils => new
                    {
                        IllnessSymptoms = ils.Symptom.Name
                    }).ToList(),
                    Products = i.ProductIllnesses.Select(pr => new
                    {
                        ProductId = pr.Product.Id,
                        ProductName = pr.Product.Name
                    }).ToList()
                }).ToList();
            

            if (illness == null)
            {
                return NotFound();
            }

            return Ok(illness);
        }

        // PUT: api/Illness/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIllness(int id, Illness illness)
        {
            if (id != illness.Id)
            {
                return BadRequest();
            }

            _context.Entry(illness).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IllnessExists(id))
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

        // POST: api/Illness
        [HttpPost]
        public async Task<ActionResult<Illness>> PostIllness(Illness illness)
        {
            _context.Illnesses.Add(illness);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIllness", new { id = illness.Id }, illness);
        }

        // DELETE: api/Illness/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Illness>> DeleteIllness(int id)
        {
            var illness = await _context.Illnesses.FindAsync(id);
            if (illness == null)
            {
                return NotFound();
            }

            _context.Illnesses.Remove(illness);
            await _context.SaveChangesAsync();

            return illness;
        }

        private bool IllnessExists(int id)
        {
            return _context.Illnesses.Any(e => e.Id == id);
        }
    }
}
