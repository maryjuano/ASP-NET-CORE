using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stark.Data.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Stark.Data;

namespace Stark
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
            services.AddDbContext<StarkDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<StarkIdentityUser, StarkIdentityRole>()
                .AddEntityFrameworkStores<StarkDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
           {
               options.Password.RequireDigit = true;
               options.Password.RequiredLength = 6;
               options.Password.RequireLowercase = false;
               options.Password.RequireUppercase = false;
               options.Password.RequireNonAlphanumeric = false;

               // Lockout settings
               options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
               options.Lockout.MaxFailedAccessAttempts = 10;
               options.Lockout.AllowedForNewUsers = true;

               // User settings
               //options.User.RequireUniqueEmail = true;
           });



            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Auth/";
                options.LogoutPath = "/Auth/Logout";
                options.AccessDeniedPath = "/Auth/AccessDenied";
                options.SlidingExpiration = true;
                options.Cookie = new CookieBuilder
                {
                    HttpOnly = true,
                    Name = ".Fiver.Security.Cookie",
                    Path = "/",
                    SameSite = SameSiteMode.Lax,
                    SecurePolicy = CookieSecurePolicy.SameAsRequest
                };
            });

            // Add Database Initializer
            services.AddScoped<IDbInitializer, DbInitializer>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IDbInitializer dbInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            dbInitializer.Initialize();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Auth}/{action=Index}/{id?}");
            });
        }
    }
}
