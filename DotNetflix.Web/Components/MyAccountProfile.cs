using DotNetflix.Web.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.Web.Components
{
    public class MyAccountProfile : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public MyAccountProfile(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            return View(user);
        }
    }
}
