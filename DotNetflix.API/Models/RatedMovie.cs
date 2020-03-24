using DotNetflix.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.API.Models
{
    public class RatedMovie
    {
        public int RatingId { get; set; }
        public string MovieId { get; set; }
        public Movie Movie { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int UserRating { get; set; }
        public string ReviewText { get; set; }
    }
}
