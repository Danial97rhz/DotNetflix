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
        public string Plot { get; set; }
        public string Poster { get; set; }
        public string Director { get; set; }
        public string Released { get; set; }        
        public Movies Movie { get; set; }
        public string Actors { get; set; }
    }
}
