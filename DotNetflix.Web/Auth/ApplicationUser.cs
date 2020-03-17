using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.Web.Auth
{
    public class ApplicationUser : IdentityUser<int>
    {
        public DateTime BirthDate { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
