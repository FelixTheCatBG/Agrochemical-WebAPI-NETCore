using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgrochemicalAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Notes { get; set; }

        public int Type { get; set; }

        public string DisplayName { get; set; }

        public virtual List<TokenModel> Tokens { get; set; }
    }
}
