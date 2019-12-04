using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgrochemicalAPI.Models
{
    public class IllnessSymptom
    {
        public int IllnessId { get; set; }
        public Illness Illness { get; set; }

        public int SymptomId { get; set; }
        public Symptom Symptom { get; set; }
    }
}
