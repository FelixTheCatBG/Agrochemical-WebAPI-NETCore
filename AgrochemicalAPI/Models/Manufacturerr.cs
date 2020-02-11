using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgrochemicalAPI.Models
{
    public class Manufacturerr
    {
        public int ManufacturerrId { get; set; }

        public string Name { get; set; }

        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
