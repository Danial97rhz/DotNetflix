using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using DotNetflix.Web.Models;
using DotNetflix.Web.ViewModels;

namespace DotNetflix.Web.Controllers
{
    public class UserMovieController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        public UserMovieController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SaveMovieRating()
        {
            return View();
        }
        public async Task<IActionResult> GetWishlist(int userId)
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:51044/api/movie/getusermovies/{userId}");
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
            return View();
        }

        public async Task<IActionResult> GetRatedMovies(int userId)
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:51044/api/movie/getusermovies/{userId}");
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
            return View();
        }
        
    }
}