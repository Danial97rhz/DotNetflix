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
            if (userManager.FindByEmailAsync("admin@dotnetflix.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = "admin",
                    Email = "admin@dotnetflix.com",
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "Password1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
            if (userManager.FindByEmailAsync("erik@dotnetflix.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = "Erik",
                    Email = "erik@dotnetflix.com",
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "Password1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }
            if (userManager.FindByEmailAsync("erica@dotnetflix.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = "Erica",
                    Email = "erica@dotnetflix.com",
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "Password1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }
            if (userManager.FindByEmailAsync("daniel@dotnetflix.com").Result == null)
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = "Daniel",
                    Email = "daniel@dotnetflix.com",
                    EmailConfirmed = true
                };

                IdentityResult result = userManager.CreateAsync(user, "Password1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }
        }
    }
}
