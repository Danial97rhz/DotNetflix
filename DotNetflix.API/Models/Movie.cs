using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.API.Models
{
    public class Movie
    {
        public string Id { get; set; }
        public float Rating { get; set; }
        public int? NumberOfVotes { get; set; }
        public string LongPlot { get; set; }
        public string PosterUrl { get; set; }
        public string Director { get; set; }
        public string ReleaseDate { get; set; }
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public int? Year { get; set; }
        public int? RunTime { get; set; }
        public bool IsAdult { get; set; }
        public List<string> Actors { get; set; }
        public string Country { get; set; }
        //public List<UserMovies> Movies { get; set; }
        // DLm: Changed List<Genre> => List<string> to match web class MovieApi
        public IEnumerable<string> Genres { get; set; }
    }
}
