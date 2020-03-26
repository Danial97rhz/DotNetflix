using DotNetflix.Web.Auth;
using DotNetflix.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DotNetflix.Web.Components
{
    public partial class Login : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public Login(UserManager<ApplicationUser> userManager,
                     SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vm = new LoginComponentViewModel
            {
                User = await _userManager.GetUserAsync(HttpContext.User),
                IsSignedIn = _signInManager.IsSignedIn(HttpContext.User)
            };

            return View(vm);
        }
    }
}
