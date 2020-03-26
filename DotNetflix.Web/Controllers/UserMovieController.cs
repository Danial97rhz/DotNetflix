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
using System.Text.Encodings.Web;

namespace DotNetflix.Web.Controllers
{
    public class UserMovieController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        private readonly IConfiguration _config;

        private readonly string UserAPIRoot;

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly HtmlEncoder _htmlEncoder;
        private readonly UserManager<ApplicationUser> _userManager;

        [BindProperty]
        public RatedMovieOut MovieRating { get; set; }

        public UserMovieController(
            IHttpClientFactory clientFactory, 
            IConfiguration config, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            HtmlEncoder htmlEncoder)
        {
            _clientFactory = clientFactory;
            _config = config;
            UserAPIRoot = _config.GetValue(typeof(string), "UserAPIRoot").ToString();
            _signInManager = signInManager;
            _htmlEncoder = htmlEncoder;
            _userManager = userManager;
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
        public async Task<IActionResult> Wishlist()
        {
            var userId = Convert.ToInt32(_userManager.GetUserId(User));

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

                //Redirect to personal wishlist
                return RedirectToAction("MyAccount", "Account", new { view = "Wishlist" });
            }
            return View();
        }

        [Authorize]
        public async Task<IActionResult> RemoveWishlistMovie(int id)
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{UserAPIRoot}DeleteWishlistMovie/{id}");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "DotNetflix.Web");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Wishlist", "UserMovie");
            }
            else
            {
                return BadRequest("Delete failed");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToRatedMovies(RatedMovieOut ratedMovie)
        {
            ratedMovie.UserId = Convert.ToInt32(_userManager.GetUserId(User)); 
            ratedMovie.UserName = _userManager.GetUserName(User);

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

            return RedirectToAction("MyAccount", "Account", new { view = "Rated movies"} );
        }


        public async Task<IActionResult> AddToWishlist(string movieId)
        {
            WishlistOut wishlistMovie = new WishlistOut();
            wishlistMovie.DateAdded = DateTime.UtcNow;
            wishlistMovie.UserId = Convert.ToInt32(_userManager.GetUserId(User));
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

        /* Metod för att visa rate movies */
        public async Task<IActionResult> RatedMovies()
        {
            int userId = Convert.ToInt32(_userManager.GetUserId(User));

            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{UserAPIRoot}GetRatedMovieList/{userId}");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "DotNetflix.Web");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                List<RateMovieViewModel> movies = await JsonSerializer.DeserializeAsync<List<RateMovieViewModel>>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                //List<RateMovieViewModel> ratedMovieVM = movies;

                return View(movies);
            }
            return View();
        }
    }
}