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
    public class DiseaseController : ControllerBase
    {
        private readonly AgrochemicalDbContext _context;

        public DiseaseController(AgrochemicalDbContext context)
        {
            _context = context;
        }

        // GET: api/Disease
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Disease>>> GetIllnesses()
        {
            var result = _context.Diseases                   
                      .Select(i => new
                      {
                          DiseaseId = i.Id,
                          DiseaseName = i.Name,
                          DiseaseDescription = i.Description,
                          //Products = i.ProductDiseases.Select(pi => new
                          //{
                          //    ProductId = pi.Product.Id,
                          //    IllnessName = pi.Product.Name
                          //}).ToList()
                          Symptoms = i.DiseaseSymtpoms.Select(ils => new
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

        // GET: api/Disease/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Disease>> GetIllness(int id)
        {
            var illness = _context.Diseases
                .Where(i => i.Id == id)
                .Select(i => new
                {
                    DiseaseId = i.Id,
                    DiseaseName = i.Name,
                    DiseaseDescription = i.Description,
                    //Products = i.ProductDiseases.Select(pi => new
                    //{
                    //    ProductId = pi.Product.Id,
                    //    IllnessName = pi.Product.Name
                    //}).ToList()
                    Symptoms = i.DiseaseSymtpoms.Select(ils => new
                    {
                        IllnessSymptoms = ils.Symptom.Name
                    }).ToList(),
                    //Products = i.ProductDiseases.Select(pr => new
                    //{
                    //    ProductId = pr.Product.Id,
                    //    ProductName = pr.Product.Name
                    //}).ToList()
                }).ToList();
            

            if (illness == null)
            {
                return NotFound();
            }

            return Ok(illness);
        }

        // PUT: api/Disease/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIllness(int id, Disease illness)
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

        // POST: api/Disease
        [HttpPost]
        public async Task<ActionResult<Disease>> PostIllness(Disease illness)
        {
            _context.Diseases.Add(illness);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIllness", new { id = illness.Id }, illness);
        }

        // DELETE: api/Disease/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Disease>> DeleteIllness(int id)
        {
            var illness = await _context.Diseases.FindAsync(id);
            if (illness == null)
            {
                return NotFound();
            }

            _context.Diseases.Remove(illness);
            await _context.SaveChangesAsync();

            return illness;
        }

        private bool IllnessExists(int id)
        {
            return _context.Diseases.Any(e => e.Id == id);
        }
    }
}
