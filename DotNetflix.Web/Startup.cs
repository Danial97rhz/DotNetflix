using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using DotNetflix.Web.Context;
using Microsoft.AspNetCore.Identity;
using DotNetflix.Web.Auth;
using Microsoft.AspNetCore.Http;

namespace DotNetflix.Web
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
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddMvc();
            services.AddHttpClient();

            // Services added for Identity ============>
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>(
                //options =>
                //{
                //    options.Password.RequiredLength = 8;
                //    options.Password.RequireNonAlphanumeric = true;
                //    options.Password.RequireUppercase = true;
                //    options.User.RequireUniqueEmail = true;
                //}
                )
                .AddEntityFrameworkStores<AppDbContext>();

            //services.AddDefaultIdentity<ApplicationUser>().AddEntityFrameworkStores<AppDbContext>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //Claims-based
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("AdministratorOnly", policy => policy.RequireRole("Administrator"));
            //    options.AddPolicy("DeletePie", policy => policy.RequireClaim("Delete Pie", "Delete Pie"));
            //    options.AddPolicy("AddPie", policy => policy.RequireClaim("Add Pie", "Add Pie"));
            //    options.AddPolicy("MinimumOrderAge", policy => policy.Requirements.Add(new MinimumOrderAgeRequirement(18)));
            //});

            // <============== Services for Identity end
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Added for Identity ===================>
            app.UseAuthentication();
            app.UseAuthorization();
            // <======================================

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                // Razor pages added for Identity
                endpoints.MapRazorPages();
            });

            app.UseRewriter(new RewriteOptions().AddRedirectToHttpsPermanent());
        }
    }
}
