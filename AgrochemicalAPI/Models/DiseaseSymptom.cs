using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgrochemicalAPI.Models
{
    public class DiseaseSymptom
    {
        public int DiseaseId { get; set; }
        public Disease Disease { get; set; }

        public int SymptomId { get; set; }
        public Symptom Symptom { get; set; }
    }
}
