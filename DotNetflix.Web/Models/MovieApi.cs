using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.Web.Models
{
    public class MovieApi
    {
        public string Id { get; set; }
        public float Rating { get; set; }
        public int? NumberOfVotes { get; set; }
        public string LongPlot { get; set; }
        public string PosterUrl { get; set; } 
        public string Director { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public int? Year { get; set; }
        public int? RunTime { get; set; }
        public bool IsAdult { get; set; }
        public List<string> Actors { get; set; }
        public List<string> Genres { get; set; }
    }
}
