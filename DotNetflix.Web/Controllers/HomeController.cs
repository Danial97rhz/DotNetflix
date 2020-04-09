using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotNetflix.Web.Models;
using System.Net.Http;
using System.Text.Json;
using DotNetflix.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using DotNetflix.Web.Auth;
using DotNetflix.Web.Context;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace DotNetflix.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly string MovieAPIRoot;

        public HomeController(IHttpClientFactory clientFactory, UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            //_logger = logger;
            _clientFactory = clientFactory;
            _userManager = userManager;
            _config = config;
            MovieAPIRoot = _config.GetValue(typeof(string), "MovieAPIRoot").ToString();
        }

        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:51044/api/movie/GetCarouselData");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "DotNetflix.Web");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var carouselsdata = await JsonSerializer.DeserializeAsync<IEnumerable<Carousel>>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                var vm = new CarouselViewModel() { Carouseldata = carouselsdata };

                return View(vm);
            }
            return View(new CarouselViewModel());


        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Movies(Search search)
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{MovieAPIRoot}AllMovies/");           
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "DotNetflix.Web");

            var searchJson = JsonSerializer.Serialize(search);
            request.Content = new StringContent(searchJson, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();

                var movies = await JsonSerializer.DeserializeAsync<SearchResult>(responseStream,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                SearchResultViewModel searchResultVM = new SearchResultViewModel
                {
                    SearchResult = movies
                };

                return View(searchResultVM);
            }

            return View();
        }

        public async Task<IActionResult> Review(int id)
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{MovieAPIRoot}GetReview/{id}");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "DotNetflix.Web");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var review = await JsonSerializer.DeserializeAsync<RatedMovie>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return View(review);
            }

            return View();
        }

        public async Task<IActionResult> Reviews(ReviewPagination reviewsInfo)
        {
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{MovieAPIRoot}GetReviews/");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "DotNetflix.Web");

            var reviewInfoJson = JsonSerializer.Serialize(reviewsInfo);
            request.Content = new StringContent(reviewInfoJson, Encoding.UTF8, "application/json");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var movies = await JsonSerializer.DeserializeAsync<ReviewPagination>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                ReviewPagination vm = new ReviewPagination();
                vm  = movies ;

                return View(new ReviewsViewModel {ReviewPagination = vm } );
            }
            return View();
    }

        /* Sida som visar generella publika listor */ 
        public async Task<IActionResult> Lists(int? id)
        {
            // LIST OF AVALIBLE WISHLISTS
            // Get user ids for all users that have registered movies to their wishlist
            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:51044/api/user/GetUsersWithWishlist");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "DotNetflix.Web");

            var response = await client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return View();
            
            using var responseStream = await response.Content.ReadAsStreamAsync();
            var usersWithWishlists = await JsonSerializer.DeserializeAsync<List<int>>(responseStream,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            // Get all users that have wishlists
            var users = new List<ApplicationUser>();
            foreach (var userId in usersWithWishlists)
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user != null)
                    users.Add(user);
            }

            // DISPLAY SINGLE WISHLIST
            // Set user id for which wishlist is to be shown
            ViewData["UserId"] = id;
            ViewData["UserName"] = users.Where(x => x.Id == id).Select(x => x.UserName).FirstOrDefault();

            return View(users);           
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
