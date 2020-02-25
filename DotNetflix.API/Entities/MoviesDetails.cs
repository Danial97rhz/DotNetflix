using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.API.Entities
{
    public class MoviesDetails
    {
        public int Id { get; set; }
        public string ShortPlot { get; set; }
        public string LongPlot { get; set; }
        public string PosterUrl { get; set; }
        public string Director { get; set; }
        public DateTime? ReleaseDate { get; set; }        
        public Movies Movie { get; set; }
        public string Actors { get; set; }
    }
}
