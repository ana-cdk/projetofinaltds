using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Data;
using ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;

namespace ecommerce
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // habilita a necessidade de consentimento para uso de cookie
                options.CheckConsentNeeded = context => true;
                // adicionar using Microsoft.AspNetCore.Http;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true; //default = false
                options.Password.RequireNonAlphanumeric = false; //default = true
                options.Password.RequireUppercase = false; //default = true
                options.Password.RequireLowercase = false; //default = true               
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3); //default = 3
                options.Lockout.MaxFailedAccessAttempts = 3; //default = 5
                options.SignIn.RequireConfirmedAccount = false; //default = false
                options.SignIn.RequireConfirmedEmail = false; //default = false
                options.SignIn.RequireConfirmedPhoneNumber = false; //default = false                
            }).AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                options.LoginPath = "/Login";
                options.AccessDeniedPath = "/Login";
                options.SlidingExpiration = true;
            });

            services.AddAuthorization(options =>
            {
                //adiciona uma politica de acesso chamada isAdmin
                options.AddPolicy("isAdmin", policy =>
                    policy.RequireRole("admin"));
            });

           services.AddRazorPages(options =>
        {
            options.Conventions.AuthorizePage("/Admin", "isAdmin");
            options.Conventions.AuthorizeFolder("/Produto", "isAdmin");
            }).AddCookieTempDataProvider(options =>
            {
                options.Cookie.IsEssential = true;
            });


            services.AddMvc();

            services.AddDbContext<AppDbContext>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                             
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });

            var defaultCulture = new CultureInfo("pt-BR");
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = new List<CultureInfo> { defaultCulture },
                SupportedUICultures = new List<CultureInfo> { defaultCulture }
            };
            app.UseRequestLocalization(localizationOptions);
        }
    }
}

