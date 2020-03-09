using DotNetflix.Web.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotNetflix.Web.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    { 
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
    }
}
