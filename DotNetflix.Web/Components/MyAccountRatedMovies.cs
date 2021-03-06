﻿using DotNetflix.Web.Auth;
using DotNetflix.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotNetflix.Web.Components
{
    public class MyAccountRatedMovies : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IConfiguration _config;
        private readonly string _userAPIRoot;

        public MyAccountRatedMovies(
            UserManager<ApplicationUser> userManager,
            IHttpClientFactory clientFactory,
            IConfiguration config)
        {
            _userManager = userManager;
            _clientFactory = clientFactory;
            _config = config;
            _userAPIRoot = _config.GetValue(typeof(string), "UserAPIRoot").ToString();
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var client = _clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_userAPIRoot}GetRatedMovieList/{user.Id}");
            request.Headers.Add("Accept", "application/json");
            request.Headers.Add("User-Agent", "DotNetflix.Web");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                List<RateMovieViewModel> movies = await JsonSerializer.DeserializeAsync<List<RateMovieViewModel>>(responseStream,
                    new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return View(movies);
            }
            return View();
        }
    }
}
