using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omdb.API.Models
{
    public class MovieResponse
    {
        public List<Search> Search { get; set; }
        public string totalResults { get; set; }
        public string Response { get; set; }
    }

    public class Search
    {
        [Newtonsoft.Json.JsonProperty("Title")]
        public string Title { get; set; }

        [Newtonsoft.Json.JsonProperty("Year")]
        public string Year { get; set; }

        [Newtonsoft.Json.JsonProperty("imdbID")]
        public string imdbID { get; set; }

        [Newtonsoft.Json.JsonProperty("Type")]
        public string Type { get; set; }


        [Newtonsoft.Json.JsonProperty("Poster")]
        public string Poster { get; set; }
    }
}
