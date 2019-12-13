using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgrochemicalAPI.Data;
using AgrochemicalAPI.Models;
using AgrochemicalAPI.DTOs;

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
            /* local variable declaration */
            var illnesses = _context.Illnesses
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

            var iC = new List<IllnessDto>(illnesses);

            foreach (var i in iC)
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

            return iC;
        }
    }
}
