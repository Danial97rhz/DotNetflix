using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.API.ModelsDto
{
    public class MovieDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int? Year { get; set; }
        [Newtonsoft.Json.JsonProperty("Poster")]
        public string PosterUrl { get; set; }
        [Newtonsoft.Json.JsonProperty("Plot")]
        public string LongPlot { get; set; }
        [Newtonsoft.Json.JsonProperty("Director")]
        public string Director { get; set; }
        [Newtonsoft.Json.JsonProperty("Released")]
        public string ReleaseDate { get; set; }
        [Newtonsoft.Json.JsonProperty("Actors")]
        public string Actors { get; set; }
        public List<string> Genres { get; set; }
    }
}
