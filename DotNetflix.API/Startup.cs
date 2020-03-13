using System;
using DotNetflix.API.Context;
using DotNetflix.API.Entities;
using DotNetflix.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DotNetflix.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Add DbContext to dependecy injection
            services.AddDbContext<DotNetflixDbContext>(options =>
            {
                options.UseSqlServer(
                    "Server=(localdb)\\MSSQLLocalDB;Database=DotNetflixDb;Trusted_Connection=True;");
            });

            // Identity
            services.AddIdentity<ApplicationUser, ApplicationRole>(
                //options =>
                //{
                //    options.Password.RequiredLength = 8;
                //    options.Password.RequireNonAlphanumeric = true;
                //    options.Password.RequireUppercase = true;
                //    options.User.RequireUniqueEmail = true;
                //}
                )
                .AddEntityFrameworkStores<DotNetflixDbContext>();

            // Add movie repository to dependency injection
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // Added for Identity ===================>
            app.UseAuthentication();
            app.UseAuthorization();
            // <======================================

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
