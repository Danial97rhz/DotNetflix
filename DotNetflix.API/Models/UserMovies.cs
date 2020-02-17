using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.API.Models
{
    public class UserMovies
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public string MovieId { get; set; }
        public Movie Movie { get; set; }
        [Range(1, 10)]
        public byte? Rating { get; set; }
        public string Comment { get; set; }
        public bool AddedToWishList { get; set; } = false;
        public DateTime Date { get; set; }
    }
}
