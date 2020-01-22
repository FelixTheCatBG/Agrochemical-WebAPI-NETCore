using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgrochemicalAPI.Models
{
    public class Symptom
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<DiseaseSymptom> DiseaseSymptoms { get; set; } = new HashSet<DiseaseSymptom>();
    }
}
