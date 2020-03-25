using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.API.Models
{
    public class SearchResult
    {
        public List<Movie> Movies { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public string Title { get; set; }

    }
}
