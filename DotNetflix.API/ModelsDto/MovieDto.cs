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
        public List<string> Genres { get; set; }
    }
}
