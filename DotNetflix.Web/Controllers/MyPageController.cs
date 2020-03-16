using Microsoft.AspNetCore.Mvc;

namespace DotNetflix.Web.Controllers
{
    public class MyPageController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }
    }
}
