using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.API.Entities
{
    public class WishlistMovies
    {
        public int Id { get; set; }
        public string MovieId { get; set; }
        public Movies Movie { get; set; }
        public int UserId { get; set; }
        //public User User { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
