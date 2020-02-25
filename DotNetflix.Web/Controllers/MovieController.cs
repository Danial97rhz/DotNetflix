using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using DotNetflix.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using DotNetflix.Web.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotNetflix.Web.Controllers
{
    public class MovieController : Controller
    {
        //private readonly IMovieData movieData;
        //private readonly IMovieRepository moviesRepository;
        private readonly IHttpClientFactory _clientFactory;


        public MovieController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<IActionResult> List(string title)
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:51044/api/movies/{title}");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "DotNetflix.Web");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var movies = await JsonSerializer.DeserializeAsync<IEnumerable<MovieApi>>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                var vm = new MovieListViewModel() { Movies = movies };

                return View(vm);
            }

            return View(new MovieListViewModel());
        }

        ///* Get movies from sql server via api without http client.
        // Get the data just by refferensing the api project and calling the 
        // repository methods directly.*/
        //public ViewResult List(string title)
        //{
        //    // Get movies from api repository
        //    var moviesFromRepo = moviesRepository.GetMovies(title);

        //    // Map movies from repo to web movie type
        //    var movies = moviesFromRepo.Select(m => new MovieApi
        //    {
        //        Id = m.Id,
        //        Title = m.Title,
        //        Year = m.Year
        //    }).ToList();

        //    // Place movies in view model for movies
        //    var vm = new MovieListViewModel
        //    {
        //        Movies = movies
        //    };

        //    // Show veiw for movies
        //    return View(vm);
        //}

       public IActionResult ListByGenre(int genreid)
        {
            // GET LIST OF MOVIES BASED ON GENRE ID AND RETURN IN VIEW.
            // LIST SHOULD BE LIMITED TO < 100 Results.

            return View();
        }

    }
}
