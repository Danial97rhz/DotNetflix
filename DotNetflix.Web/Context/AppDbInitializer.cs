using DotNetflix.Web.Auth;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetflix.Web.Context
{
    public static class AppDbInitializer
    {
        public static void SeedUser(UserManager<ApplicationUser> userManager)
        {
            if (userManager.FindByEmailAsync("admin@dotnetflix.com").Result==null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = "admin",
                    Email = "admin@dotnetflix.com"
                };

                IdentityResult result = userManager.CreateAsync(user, "Password1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }
    }
}
