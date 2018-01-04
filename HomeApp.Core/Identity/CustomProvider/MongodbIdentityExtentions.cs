using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace HomeApp.Core.Identity.CustomProvider
{
    public static class MongodbIdentityExtentions
    {
        public static IdentityBuilder AddMongoDbStores(this IServiceCollection services, string connectionString)
        {
            IdentityBuilder builder = services.AddIdentity<IdentityUser, IdentityRole>();
            builder.Services.AddSingleton<IUserStore<IdentityUser>>(s => new UserStore(connectionString));
            builder.Services.AddSingleton<IRoleStore<IdentityRole>>(r => new RoleStore(connectionString));

            return builder;
        }
    }
}
