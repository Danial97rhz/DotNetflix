using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.Web.Models
{
    public class MovieApi
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int? Year { get; set; }
        public List<string> Genres { get; set; }
    }
}
