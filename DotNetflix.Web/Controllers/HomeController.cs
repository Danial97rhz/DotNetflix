using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DotNetflix.Web.Models;
using DotNetflix.API.Services;
using Omdb.API;

namespace DotNetflix.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMailService _mailService;
        private readonly IMovieRepository _repository;
        private readonly IOmdbRepository _omdbRepository;

        public HomeController(ILogger<HomeController> logger, 
            IMailService mailService,
            IMovieRepository repository,
            IOmdbRepository omdbRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _repository = repository;
            _omdbRepository = omdbRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        



        // SEARCH Omdb
        public async Task<IActionResult> Search(string q)
        {
            var result = await _omdbRepository.Search(q);
            ViewData["Search"] = q;

            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
