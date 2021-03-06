using System;
using System.Collections.Generic;
using System.Security.Claims;
using HomeApp.Core.Identity.CustomProvider;
using HomeApp.Core.Repositories;
using HomeApp.Core.Repositories.Contracts;
using HomeApp.Site.Filters;
using HomeApp.Site.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HomeApp.Site
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
            AppOptions appOptions = Configuration.GetSection("AppOptions").Get<AppOptions>();
            string con = Configuration.GetConnectionString("MongoDb");

            services.AddIdentityMongoDbStores(con).AddDefaultTokenProviders();
            services.AddAuthentication().AddVK(options =>
            {
                options.ClientId = Configuration["Authentication:Vk:ClientId"];
                options.ClientSecret = Configuration["Authentication:Vk:ClientSecret"];

                options.Scope.Add("email");

                options.Fields.Add("uid");
                options.Fields.Add("first_name");
                options.Fields.Add("last_name");

                options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "uid");
                options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                options.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "first_name");
                options.ClaimActions.MapJsonKey(ClaimTypes.Surname, "last_name");
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "HomeApp";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.LoginPath = "/auth/login";
                options.LogoutPath = "/auth/logout";
                options.AccessDeniedPath = "/auth";
                options.SlidingExpiration = true;
                options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            });

            services.AddMvc();

            services.AddRouting(options => { options.LowercaseUrls = true; });

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddTransient<IEmailSenderRepository, EmailSenderRepository>(options =>
            {
                IHostingEnvironment env = options.GetService<IHostingEnvironment>();
                return new EmailSenderRepository($"{env.ContentRootPath}/RazorTemplates/Emails");
            });
            services.AddSingleton<IAdRepository, AdRepository>();
            services.AddSingleton<IUserRepository, UserRepository>(options=> new UserRepository(con));
            services.AddSingleton<IRealEstateRepository, RealEstateRepository>(options=> new RealEstateRepository(con));

            services.AddScoped<UserProfileFilter>();

            services.AddResponseCompression();
            services.AddResponseCaching();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseStatusCodePages();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc();
            app.UseResponseCompression();
            //app.UseResponseCaching();
        }
    }
}
