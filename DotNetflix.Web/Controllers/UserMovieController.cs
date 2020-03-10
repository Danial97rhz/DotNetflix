using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using DotNetflix.Web.Models;
using DotNetflix.Web.ViewModels;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace DotNetflix.Web.Controllers
{
    public class UserMovieController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        private readonly IConfiguration _config;

        private readonly string UserAPIRoot;

        public UserMovieController(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
            UserAPIRoot = _config.GetValue(typeof(string), "UserAPIRoot").ToString();
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
            var request = new HttpRequestMessage(HttpMethod.Get, $"{UserAPIRoot}GetWishlist/{userId}");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "DotNetflix.Web");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {

                using var responseStream = await response.Content.ReadAsStreamAsync();
                var movies = await JsonSerializer.DeserializeAsync<IEnumerable<Wishlist>>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                var wishlistVM = new WishlistViewModel() { WishlistMovies = movies };

                return View(wishlistVM);
            }
            return View();
        }

        public async Task<IActionResult> AddToWishlist(Wishlist wishlistMovie)
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{UserAPIRoot}PostWishlistMovie");

            wishlistMovie.DateAdded = DateTime.UtcNow;
            wishlistMovie.UserId = 1; //Remove after login stuff is implemented

            var movieJson = JsonSerializer.Serialize(wishlistMovie);
            request.Content = new StringContent(movieJson, Encoding.UTF8, "application/json");
            request.Headers.Add("User-Agent", "DotNetflix.Web");
            var response = await client.SendAsync(request);


            if (!response.IsSuccessStatusCode)
            {
                TempData["PostError"] = "Something went wrong, try again or contact support!";
            }

            return View();
        }


        public async Task<IActionResult> GetRatedMovies(int userId)
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{UserAPIRoot}GetRatedMovieList/{userId}");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "DotNetflix.Web");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {


                using var responseStream = await response.Content.ReadAsStreamAsync();
                var movies = await JsonSerializer.DeserializeAsync<IEnumerable<RatedMovie>>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                var ratedMovieVM = new RateMovieViewModel() { RatedMovies = movies };

                return View(ratedMovieVM);
            }
            return View();
        }

        public async Task<IActionResult> AddToRatedMovies(RatedMovie ratedMovie)
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{UserAPIRoot}PostRatedMovie");

            ratedMovie.UserId = 1; //Remove after login stuff is implemented

            var movieJson = JsonSerializer.Serialize(ratedMovie);
            request.Content = new StringContent(movieJson, Encoding.UTF8, "application/json");
            request.Headers.Add("User-Agent", "DotNetflix.Web");
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                TempData["PostError"] = "Something went wrong, try again or contact support!";
            }

            return View();
        }

    }
}