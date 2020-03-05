using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DotNetflix.Web.Controllers
{
    public class UserMovieController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SaveMovieRating()
        {
            return View();
        }
    }
}