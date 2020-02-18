using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetflix.Web.Data;
using DotNetflix.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotNetflix.Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieData movieData;

        public MovieController(IMovieData movieData)
        {
            this.movieData = movieData;
        }

        // GET: /<controller>/
        public IActionResult List(string title)
        {
            var movies = movieData.GetMoviesByTitle(title);
            var vm = new MovieListViewModel
            {
                Movies = movies
            };
            return RedirectToPage("/Movie/List", vm);
        }
    }
}
