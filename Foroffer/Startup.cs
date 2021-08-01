using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Foroffer.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace Foroffer
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment _env)
        {
            Configuration = configuration;
            Environment = _env;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                var cookieOptions = new Microsoft.AspNetCore.Http.CookieOptions()
                {
                    Path = "/",
                    HttpOnly = false,
                    IsEssential = true, //<- there
                    Expires = DateTime.Now.AddMonths(1),
                };
            });

            //Add Localization
            services.AddLocalization(opts =>
            {
                opts.ResourcesPath = "Resources";
            });

            services.AddDbContext<ForofferDbContext>(x =>
            {
                x.UseSqlServer(Configuration["Database:ForofferDb"]);
            });

            // for FileUpload
            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                   Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")
                    ));

            services.AddIdentity<AppUser, IdentityRole>(options => {
                // options.SignIn.RequireConfirmedEmail = true;
                #region Password configuration
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                #endregion
                options.Lockout.MaxFailedAccessAttempts = 9;
                // options.User.AllowedUserNameCharacters = "qwertyuiopasdfghjklzxcvbnm";
                options.User.RequireUniqueEmail = true;


            })
                                      .AddEntityFrameworkStores<ForofferDbContext>()
                                        .AddDefaultTokenProviders();

            services.AddMvc().
               AddViewLocalization(opts =>
               {
                   opts.ResourcesPath = "Resources";
               }).AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix, opts => { opts.ResourcesPath = "Resources"; }).
                AddDataAnnotationsLocalization().
               SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddAntiforgery(a => a.HeaderName = "XSRF-TOKEN");

            services.Configure<RequestLocalizationOptions>(opts =>
            {
                var supportedCultures = new List<CultureInfo>
                {
                    new CultureInfo("az-Latn-AZ"),
                    new CultureInfo("en-GB")
                };

                opts.DefaultRequestCulture = new RequestCulture("az-Latn-AZ");
                opts.SupportedCultures = supportedCultures;
                opts.SupportedUICultures = supportedCultures;
                // services.AddSingleton(opts);
            });

            if (!Environment.IsDevelopment())
            {
                services.AddHttpsRedirection(options =>
                {
                    options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
                    options.HttpsPort = 443;
                });
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //Request Localization
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                        name: "root",
                        template: "{action}/{id?}",
                        defaults: new { controller = "Home", action = "Index" });

                routes.MapRoute(
                        name: "detailed",
                        template: "{action}/{id?}",
                        defaults: new { controller = "Detailed", action = "Detailed" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
