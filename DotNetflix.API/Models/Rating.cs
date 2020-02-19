using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.API.Models
{
    public class Rating
    {
        public int Id { get; set; }
        [Range(1, 10)]
        public float AvrageRating { get; set; }
        public int NumberOfVotes { get; set; }
        public List<Movie> Movies { get; set; }
    }
}
