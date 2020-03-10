using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DotNetflix.API.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public DateTime BirthDate { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
