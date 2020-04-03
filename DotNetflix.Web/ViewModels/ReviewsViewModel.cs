using DotNetflix.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.Web.ViewModels
{
    [BindProperties]
    public class ReviewsViewModel
    {
        public ReviewPagination ReviewPagination { get; set; }
        [BindProperty(SupportsGet = true)]

        public bool ShowNext => ReviewPagination.CurrentPage < ReviewPagination.TotalPages;
        public bool ShowPrevious => ReviewPagination.CurrentPage > 1;
        public bool ShowFirst => ReviewPagination.CurrentPage != 1;
        public bool ShowLast => ReviewPagination.CurrentPage < ReviewPagination.TotalPages;
    }
}
