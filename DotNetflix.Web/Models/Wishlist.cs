using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.Web.Models
{
    public class Wishlist
    {
        public int Id { get; set; }
        public Movie Movie { get; set; }
        public int UserId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
