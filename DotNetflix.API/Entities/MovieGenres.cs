using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.API.Entities
{
    public class MovieGenres
    {
        public int GenresId { get; set; }
        public string MoviesId { get; set; }
        public Genres Genre { get; set; }
        public Movies Movie { get; set; }
    }
}
