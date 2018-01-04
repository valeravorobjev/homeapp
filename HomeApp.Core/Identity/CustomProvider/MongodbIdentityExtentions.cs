using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace HomeApp.Core.Identity.CustomProvider
{
    public static class MongodbIdentityExtentions
    {
        public static IdentityBuilder AddIdentityMongoDbStores(this IServiceCollection services, string connectionString)
        {
            IdentityBuilder builder = services.AddIdentity<CustomIdentityUser, CustomIdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                options.SignIn.RequireConfirmedEmail = true;

                options.User.RequireUniqueEmail = true;
            });
            builder.Services.AddSingleton<IUserStore<CustomIdentityUser>>(s => new CustomUserStore(connectionString));
            builder.Services.AddSingleton<IRoleStore<CustomIdentityRole>>(r => new CustomRoleStore(connectionString));

            return builder;
        }
    }
}
