using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgrochemicalAPI.DTOs
{
    public class ProductCategoryResult
    {
        public string Name { get; set; }
        public ICollection<ProductResult> ProductResults{ get; set; }
    }
}
