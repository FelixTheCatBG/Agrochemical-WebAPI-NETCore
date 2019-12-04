using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgrochemicalAPI.Models
{
    public class CropIllness
    {
        public int CropId { get; set; }
        public Crop Crop { get; set; }

        public int IllnessId { get; set; }
        public Illness Illness { get; set; }
    }
}
