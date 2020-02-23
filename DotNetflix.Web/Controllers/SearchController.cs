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
    public class SearchController : Controller
    {
        private readonly ILogger<SearchController> _logger;
        private readonly IMailService _mailService;
        private readonly IMovieRepository _repository;
        private readonly IOmdbRepository _omdbRepository;

        public SearchController(ILogger<SearchController> logger, 
            IMailService mailService,
            IMovieRepository repository,
            IOmdbRepository omdbRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _repository = repository;
            _omdbRepository = omdbRepository;
        }


        // SEARCH Omdb
        public async Task<IActionResult> Index(string title)
        {
            var result = await _omdbRepository.Search(title);
            ViewData["Search"] = title;

            return View(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
