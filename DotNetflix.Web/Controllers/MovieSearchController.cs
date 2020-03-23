using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DotNetflix.Web.Models;
using DotNetflix.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DotNetflix.Web.Controllers
{
    public class MovieSearchController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;

        private readonly IConfiguration _config;

        private readonly string MovieAPIRoot;

        public MovieSearchController(IHttpClientFactory clientFactory, IConfiguration config)
        {
            _clientFactory = clientFactory;
            _config = config;
            MovieAPIRoot = _config.GetValue(typeof(string), "MovieAPIRoot").ToString();
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> SearchResult(Search search)
        {
            if (string.IsNullOrEmpty(search.Title)) search.Title = "abc";

            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{MovieAPIRoot}GetPaginatedMovies");
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
                SearchResultViewModel searchResultVM = new SearchResultViewModel();

                searchResultVM.SearchResult = movies;

                return View(searchResultVM);
            }
            else
            {
                return View();
            }

        }
    }
}