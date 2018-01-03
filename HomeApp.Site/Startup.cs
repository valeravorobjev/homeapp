using System.Collections.Generic;
using HomeApp.Core.Repositories;
using HomeApp.Core.Repositories.Contracts;
using HomeApp.Core.Repositories.Fake;
using HomeApp.Site.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Converters = new List<JsonConverter> { new BsonIdConverter() };
            });

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
            app.UseMvc();
            app.UseResponseCompression();
            //app.UseResponseCaching();
        }
    }
}
