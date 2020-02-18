using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.Web.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public String FristName { get; set; }
        [Required]
        public String LastName { get; set; }
        public int BirthYear { get; set; }
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public List<UserMovies> Movies { get; set; }
    }
}
