using DotNetflix.Web.Auth;
using DotNetflix.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DotNetflix.Web.Components
{
    public partial class Login : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public Login(UserManager<ApplicationUser> userManger,
                     SignInManager<ApplicationUser> signInManager)
        {
            _userManger = userManger;
            _signInManager = signInManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vm = new LoginComponentViewModel
            {
                User = await _userManger.GetUserAsync(HttpContext.User),
                IsSignedIn = _signInManager.IsSignedIn(HttpContext.User)
            };

            return View(vm);
        }
    }
}
