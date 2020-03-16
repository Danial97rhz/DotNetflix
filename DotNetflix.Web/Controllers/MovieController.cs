using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using DotNetflix.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using DotNetflix.Web.Models;
using Microsoft.Extensions.Configuration;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DotNetflix.Web.Controllers
{
    public class MovieController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        private readonly IConfiguration _config;

        private readonly string MovieAPIRoot;

        public MovieController(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
            MovieAPIRoot = _config.GetValue(typeof(string), "MovieAPIRoot").ToString();
        }

        public async Task<IActionResult> List(string title)        
        {
            if (string.IsNullOrEmpty(title)) title = "abc";

            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{MovieAPIRoot}getmovies/{title}");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "DotNetflix.Web");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var movies = await JsonSerializer.DeserializeAsync<IEnumerable<Movie>>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                var vm = new MovieListViewModel() { Movies = movies };

                return View(vm);
            }
            return View(new MovieListViewModel());
        }

        public async Task<IActionResult> MovieInfo(string movieId)
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{MovieAPIRoot}getmovie/{movieId}");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "DotNetflix.Web");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var movie = await JsonSerializer.DeserializeAsync<Movie>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                var vm = new MovieInfoViewModel() { Movie = movie };

                return View(vm);
            }
            return View();
        }

       public async Task<IActionResult> ListByGenre(int genreId)
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{MovieAPIRoot}GetMoviesByGenre/{genreId}");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "DotNetflix.Web");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var movies = await JsonSerializer.DeserializeAsync<IEnumerable<Movie>>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    var vm = new MovieListViewModel() { Movies = movies };
                    return View(vm);               
                }
            }
            // GET LIST OF MOVIES BASED ON GENRE ID AND RETURN IN VIEW.
            // LIST SHOULD BE LIMITED TO < 100 Results.

            return View();
        }

        public async Task<IActionResult> RateMovie(string movieId)
        {            
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{MovieAPIRoot}getmovie/{movieId}");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "DotNetflix.Web");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var movie = await JsonSerializer.DeserializeAsync<Movie>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                var vm = new MovieInfoViewModel() { Movie = movie };

                return View(vm);
            }
            return View();
        }

        public async Task<IActionResult> ListAdult(bool isAdult)
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{MovieAPIRoot}GetAdultMovies/{isAdult}");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "DotNetflix.Web");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var movies = await JsonSerializer.DeserializeAsync<IEnumerable<Movie>>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                var vm = new MovieListViewModel() { Movies = movies };

                return View(vm);
            }
            return View(new MovieListViewModel());
        }

    }
}
