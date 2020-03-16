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
using Microsoft.AspNetCore.Identity;
using DotNetflix.Web.Auth;
using Microsoft.AspNetCore.Authorization;

namespace DotNetflix.Web.Controllers
{
    public class UserMovieController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        private readonly IConfiguration _config;

        private readonly string UserAPIRoot;

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        [BindProperty]
        public RatedMovieOut MovieRating { get; set; }

        public UserMovieController(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
            UserAPIRoot = _config.GetValue(typeof(string), "UserAPIRoot").ToString();
            //_signInManager = signInManager;
            //_userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SaveMovieRating()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Wishlist(int userId)
        {
            //var userId = Convert.ToInt32._userManager.GetUserId(User);

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

        [HttpPost]
        public async Task<IActionResult> AddToRatedMovies(RatedMovieOut ratedMovie)
        {
            ratedMovie.UserId = 1; //Remove after login stuff is implemented

            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{UserAPIRoot}PostRatedMovie");

            var movieJson = JsonSerializer.Serialize(ratedMovie);
            request.Content = new StringContent(movieJson, Encoding.UTF8, "application/json");
            request.Headers.Add("User-Agent", "DotNetflix.Web");
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                TempData["PostError"] = "Something went wrong, try again or contact support!";
            }

            return RedirectToAction("RatedMovies", "UserMovie");
        }


        public async Task<IActionResult> AddToWishlist(string movieId)
        {
            WishlistOut wishlistMovie = new WishlistOut();
            wishlistMovie.DateAdded = DateTime.UtcNow;
            wishlistMovie.UserId = 1; //Remove after login stuff is implemented
            wishlistMovie.MovieId = movieId;

            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{UserAPIRoot}PostWishlistMovie");

            var movieJson = JsonSerializer.Serialize(wishlistMovie);
            request.Content = new StringContent(movieJson, Encoding.UTF8, "application/json");
            request.Headers.Add("User-Agent", "DotNetflix.Web");
            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                TempData["PostError"] = "Something went wrong, try again or contact support!";
            }

            return RedirectToAction("Wishlist", "UserMovie");
        }


        public async Task<IActionResult> RatedMovies(int userId = 1)
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



    }
}