using Auth.IdentityProvider;
using Auth.Models;
using DataAccess;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Data.SqlClient;

namespace Auth
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "Auth";
                    options.LoginPath = new PathString("/Account/Login");
                    options.LogoutPath = new PathString("/Account/Logout");
                    options.AccessDeniedPath = new PathString("/Home/NotFound");
                    options.ExpireTimeSpan = new TimeSpan(7, 0, 0, 0);
                });

            services.AddIdentity<AppUser, AppRole>()
            .AddUserStore<CustomUserStore>()
            .AddRoleStore<CustomRoleStore>()
            .AddUserManager<UserManager<AppUser>>()
            .AddDefaultTokenProviders();

            services.AddControllersWithViews();

            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddTransient<SqlConnection>(e => new SqlConnection(connectionString));

            services.AddScoped<Context<AppUser>>(e => new Context<AppUser>(connectionString));
            services.AddScoped<Context<AppRole>>(a => new Context<AppRole>(connectionString));

            services.AddScoped<CustomUserStore>();
            services.AddScoped<CustomRoleStore>();


            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {                
                app.UseHsts();
            }
           // app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
