using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgrochemicalAPI.Models
{
    public class CropCategory
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Crop> Crops { get; set; } = new HashSet<Crop>();
    }
}
