using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgrochemicalAPI.Data;
using AgrochemicalAPI.Models;
using AgrochemicalAPI.DTOs;
using Microsoft.EntityFrameworkCore;

namespace AgrochemicalAPI.Services
{
    public interface IDecisionBuilder
    {
         List<IllnessDto> FindIllnesses(List<int> symptoms);
    }
    public class DecisionBuilder : IDecisionBuilder
    {
        private readonly AgrochemicalDbContext _context;

        public DecisionBuilder(AgrochemicalDbContext context)
        {
            _context = context;
        }

        public List<IllnessDto> FindIllnesses(List<int> searchSymptoms)
        {
            var illnessesByCrop = _context.Illnesses.AsNoTracking();

            /* local variable declaration */
            var possibleIllnesses = illnessesByCrop
              .Where(i => searchSymptoms.All(ss => i.IllnessSymptoms.Any(ils => ss == ils.SymptomId)))
              .Select(x => new IllnessDto
              {
                  Name = x.Name,
                  Symptom = x.IllnessSymptoms.Select(ils => new Symptom
                  {
                      Id = ils.SymptomId,
                      Name = ils.Symptom.Name
                  }).ToList()
              }).ToList();

            var symptomsToEvaluate = new List<IllnessDto>(possibleIllnesses);

            foreach (var i in symptomsToEvaluate)
            {
                foreach (var s in i.Symptom.ToList())
                {
                    if (searchSymptoms.Contains(s.Id))
                    {
                        i.Symptom.Remove(s);
                    }
                }
            }

            //var fd = illnesses.GroupBy(x => x.Symptom.GroupBy(a => a.Id));

            return symptomsToEvaluate;
        }
    }
}
