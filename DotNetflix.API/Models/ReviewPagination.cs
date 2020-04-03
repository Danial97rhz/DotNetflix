using DotNetflix.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.API.Models
{
    public class ReviewPagination
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 12;
        public int Count { get; set; }
        public List<RatedMovies> RatedMovies { get; set; }
    }
}
