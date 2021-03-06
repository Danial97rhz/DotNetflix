﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.Web.Models
{
    public class SearchResult
    {
        [BindProperty(SupportsGet =true)]
        public int CurrentPage { get; set; } = 1;
        public int Count { get; set; }
        public int PageSize { get; set; } = 12;

        public int TotalPages => (int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public IEnumerable<Movie> Movies { get; set; }
        public string Title { get; set; }

        public bool ShowNext => CurrentPage < TotalPages;
        public bool ShowPrevious => CurrentPage > 1;
        public bool ShowFirst => CurrentPage != 1;
        public bool ShowLast => CurrentPage <= TotalPages;
    }
}
