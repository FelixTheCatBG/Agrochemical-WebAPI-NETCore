using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AgrochemicalAPI.Data;
using AgrochemicalAPI.Models;
using Microsoft.AspNetCore.Authorization;

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
                          Treatment = i.Treatment,
                          SymptomsDescription = i.SymptomsDescription,
                          imgPath = i.imgPath,
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
                    Treatment = i.Treatment,
                    SymptomsDescription = i.SymptomsDescription,
                    imgPath = i.imgPath,
                    Symptoms = i.DiseaseSymtpoms.Select(ils => new
                    {
                        IllnessSymptoms = ils.Symptom.Name
                    }).ToList(),
                }).ToList();
            

            if (illness == null)
            {
                return NotFound();
            }

            return Ok(illness);
        }


        [Authorize(Roles = "Admin")]
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


        [Authorize(Roles = "Admin")]
        // POST: api/Disease
        [HttpPost]
        public async Task<ActionResult<Disease>> PostIllness(Disease illness)
        {
            _context.Diseases.Add(illness);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIllness", new { id = illness.Id }, illness);
        }


        [Authorize(Roles = "Admin")]
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
