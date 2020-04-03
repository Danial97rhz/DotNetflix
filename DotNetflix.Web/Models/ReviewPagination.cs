using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.Web.Models
{
    public class ReviewPagination
    {
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 12;
        public int Count { get; set; }
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
        public List<RatedMovie> RatedMovies { get; set; }
    }
}
