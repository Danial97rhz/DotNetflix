using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.API.Models
{
    public class WishList
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime DateAdded { get; set; }
        public string MovieId { get; set; }
        public string Title { get; set; }
        public int? Year { get; set; }
        public string Rating { get; set; }


    }
}
