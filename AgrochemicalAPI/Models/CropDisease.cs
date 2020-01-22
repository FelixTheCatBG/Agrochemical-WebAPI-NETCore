using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgrochemicalAPI.Models
{
    public class CropDisease
    {
        public int CropId { get; set; }
        public Crop Crop { get; set; }

        public int DiseaseId { get; set; }
        public Disease Disease { get; set; }
    }
}
