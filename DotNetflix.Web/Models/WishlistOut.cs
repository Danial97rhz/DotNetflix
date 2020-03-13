using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.Web.Models
{
    public class WishlistOut
    {
        public int Id { get; set; }
        public string MovieId { get; set; }
        public int UserId { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
