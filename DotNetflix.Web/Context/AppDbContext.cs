using DotNetflix.Web.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DotNetflix.Web.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    { 
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // IDENTITY: Call base class OnModelCreating
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationRole>().HasData(
                new ApplicationRole { Name = "User", NormalizedName = "USER", Id = 1 },
                new ApplicationRole { Name = "Administrator", NormalizedName = "ADMINISTRATOR", Id = 2 }
                );
        }
    }
}
