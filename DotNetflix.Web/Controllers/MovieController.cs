using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetflix.Web.Data;
using DotNetflix.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using DotNetflix.API.Services;
using DotNetflix.Web.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotNetflix.Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly IMovieData movieData;
        private readonly IMovieRepository moviesRepository;

        public MovieController(
            //IMovieData movieData
            IMovieRepository moviesRepository)
        {
            //this.movieData = movieData;
            this.moviesRepository = moviesRepository;
        }


        /* Get movies from sql server via api without http client.
         Get the data just by refferensing the api project and calling the 
         repository methods directly.*/
        public ViewResult List(string title)
        {
            // Get movies from api repository
            var moviesFromRepo = moviesRepository.GetMovies(title);

            // Map movies from repo to web movie type
            var movies = moviesFromRepo.Select(m => new MovieApi
            {
                Id = m.Id,
                Title = m.Title,
                Year = m.Year
            }).ToList();

            // Place movies in view model for movies
            var vm = new MovieListViewModel
            {
                Movies = movies
            };

            // Show veiw for movies
            return View(vm);
        }

       public IActionResult ListByGenre(int genreid)
        {
            // GET LIST OF MOVIES BASED ON GENRE ID AND RETURN IN VIEW.
            // LIST SHOULD BE LIMITED TO < 100 Results.

            return View();
        }

    }
}
