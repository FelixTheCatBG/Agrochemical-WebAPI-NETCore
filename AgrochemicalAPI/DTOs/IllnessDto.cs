using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AgrochemicalAPI.Models;

namespace AgrochemicalAPI.DTOs
{
    public class IllnessDto
    {
        public string Name { get; set; }
        public ICollection<Symptom> Symptom { get; set; } = new List<Symptom>();
    }
}
