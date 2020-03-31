using DotNetflix.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.Web.ViewModels
{
    public class ReviewsViewModel
    {
        public IEnumerable<RatedMovie> RatedMovies { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;
        public int Count => RatedMovies.Count();
        public int PageSize { get; set; } = 12;
        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));
        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage < TotalPages;
    }
}
