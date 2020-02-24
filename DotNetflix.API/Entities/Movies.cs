using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.API.Entities
{
    public class Movies
    {
        public string MoviesId { get; set; }
        public int RatingsId { get; set; }
        public int MoviesDetailsId { get; set; }
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public int Year { get; set; }
        public int RunTimeMinutes { get; set; }
        public bool IsAdult { get; set; }

        public MoviesDetails Details { get; set; }
        public List<MovieGenres> MovieGenres { get; set; }
    }
}
