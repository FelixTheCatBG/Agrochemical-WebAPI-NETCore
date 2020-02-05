using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgrochemicalAPI.Models
{
    public class Crop
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<ProductCropDisease> ProductCropDiseases { get; set; } = new HashSet<ProductCropDisease>();
    }
}
