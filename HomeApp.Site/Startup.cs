using System;
using System.Collections.Generic;
using HomeApp.Core.Identity.CustomProvider;
using HomeApp.Core.Repositories;
using HomeApp.Core.Repositories.Contracts;
using HomeApp.Core.Repositories.Fake;
using HomeApp.Site.Utils;
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

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Converters = new List<JsonConverter> { new BsonIdConverter() };
            });

            services.AddRouting(options => { options.LowercaseUrls = true; });

            services.AddScoped<ISessionRepository, SessionRepository>(options => new SessionRepository(con));
            services.AddScoped<IAuthRepository, AuthRepository>(options=>new AuthRepository(options.GetService<ISessionRepository>(), con));

            services.AddSingleton<IAdRepository, AdFakeRepository>();
            services.AddSingleton<IUserRepository, UserFakeRepository>(options=> new UserFakeRepository(con));
            services.AddSingleton<IRealEstateRepository, RealEstateFakeRepository>(options=> new RealEstateFakeRepository(con));

            services.AddResponseCompression();
            services.AddResponseCaching();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseMvc();
            app.UseResponseCompression();
            //app.UseResponseCaching();
        }
    }
}
