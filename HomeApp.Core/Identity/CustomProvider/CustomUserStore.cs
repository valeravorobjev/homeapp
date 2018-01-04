using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using HomeApp.Core.Db;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace HomeApp.Core.Identity.CustomProvider
{
    public class CustomUserStore : IUserPasswordStore<CustomIdentityUser>,
        IUserRoleStore<CustomIdentityUser>,
        IUserLoginStore<CustomIdentityUser>,
        IUserSecurityStampStore<CustomIdentityUser>,
        IUserEmailStore<CustomIdentityUser>,
        IUserClaimStore<CustomIdentityUser>,
        IUserLockoutStore<CustomIdentityUser>,
        IQueryableUserStore<CustomIdentityUser>,
        IUserAuthenticationTokenStore<CustomIdentityUser>
    {
        private readonly IMongoCollection<CustomIdentityUser> _userCollection;

        public CustomUserStore(string con)
        {
            IMongoClient client = new MongoClient(con);
            IMongoDatabase db = client.GetDatabase(DbSet.DB_NAME_FAKE); // TODO:: fake!!!

            _userCollection = db.GetCollection<CustomIdentityUser>(DbSet.IDENTITY_USERS_COLLECTION);
        }

        public void Dispose()
        {
        }

        public async Task<string> GetUserIdAsync(CustomIdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.Id.ToString(), cancellationToken);
        }

        public async Task<string> GetUserNameAsync(CustomIdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.Name, cancellationToken);
        }

        public async Task SetUserNameAsync(CustomIdentityUser user, string userName, CancellationToken cancellationToken)
        {
            await Task.Run(() => user.Name = userName, cancellationToken);
        }

        public async Task<string> GetNormalizedUserNameAsync(CustomIdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.NormalizedUserName, cancellationToken);
        }

        public async Task SetNormalizedUserNameAsync(CustomIdentityUser user, string normalizedName, CancellationToken cancellationToken)
        {
            await Task.Run(() => user.NormalizedUserName = normalizedName, cancellationToken);
        }

        public async Task<IdentityResult> CreateAsync(CustomIdentityUser user, CancellationToken cancellationToken)
        {
            try
            {
                await _userCollection.InsertOneAsync(user, cancellationToken: cancellationToken);
            }
            catch
            {
                return IdentityResult.Failed();
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(CustomIdentityUser user, CancellationToken cancellationToken)
        {
            try
            {
                await _userCollection.ReplaceOneAsync(u => u.Id == user.Id, user, cancellationToken: cancellationToken);
            }
            catch
            {
                return IdentityResult.Failed();
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(CustomIdentityUser user, CancellationToken cancellationToken)
        {
            try
            {
                await _userCollection.DeleteOneAsync(u => u.Id == user.Id, cancellationToken);
            }
            catch
            {
                return IdentityResult.Failed();
            }

            return IdentityResult.Success;
        }

        public async Task<CustomIdentityUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await _userCollection.AsQueryable().FirstOrDefaultAsync(u => u.Id == ObjectId.Parse(userId), cancellationToken: cancellationToken);
        }

        public async Task<CustomIdentityUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await _userCollection.AsQueryable().FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName, cancellationToken: cancellationToken);
        }

        public async Task SetPasswordHashAsync(CustomIdentityUser user, string passwordHash, CancellationToken cancellationToken)
        {
            await Task.Run(() => user.PasswordHash = passwordHash, cancellationToken);
        }

        public async Task<string> GetPasswordHashAsync(CustomIdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.PasswordHash, cancellationToken);
        }

        public async Task<bool> HasPasswordAsync(CustomIdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
             {
                 bool hasPwd = !string.IsNullOrEmpty(user.PasswordHash);
                 return hasPwd;

             }, cancellationToken);
        }

        public async Task AddToRoleAsync(CustomIdentityUser user, string roleName, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                if (user.Roles == null) user.Roles = new List<string>();
                user.Roles.Add(roleName);
            }, cancellationToken);
        }

        public async Task RemoveFromRoleAsync(CustomIdentityUser user, string roleName, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                if (user.Roles == null) return;
                user.Roles.Remove(roleName);
            }, cancellationToken);
        }

        public async Task<IList<string>> GetRolesAsync(CustomIdentityUser user, CancellationToken cancellationToken)
        {
            if (user.Roles == null) user.Roles = new List<string>();
            return await Task.Run(() => user.Roles, cancellationToken);
        }

        public async Task<bool> IsInRoleAsync(CustomIdentityUser user, string roleName, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.Roles.Contains(roleName), cancellationToken);
        }

        public async Task<IList<CustomIdentityUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            return await _userCollection.AsQueryable().Where(u => u.Roles.Contains(roleName))
                .ToListAsync(cancellationToken);
        }

        public async Task AddLoginAsync(CustomIdentityUser user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                IdentityUserLogin identityUserLogin = new IdentityUserLogin
                {
                    LoginProvider = login.LoginProvider,
                    ProviderDisplayName = login.ProviderDisplayName,
                    ProviderKey = login.ProviderKey
                };

                if (user.Logins == null) user.Logins = new List<IdentityUserLogin>();
                user.Logins.Add(identityUserLogin);

            }, cancellationToken);
        }

        public async Task RemoveLoginAsync(CustomIdentityUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                if (user.Logins == null) return;
                user.Logins.RemoveAll(l => l.LoginProvider == loginProvider && l.ProviderKey == providerKey);

            }, cancellationToken);
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(CustomIdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.Logins.Select(l => new UserLoginInfo(l.LoginProvider, l.ProviderKey, l.ProviderDisplayName)).ToList(), cancellationToken);
        }

        public async Task<CustomIdentityUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            return await _userCollection.AsQueryable()
                .Where(u => u.Logins.Any(l => l.LoginProvider == loginProvider && l.ProviderKey == providerKey))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task SetSecurityStampAsync(CustomIdentityUser user, string stamp, CancellationToken cancellationToken)
        {
            await Task.Run(() => user.SecurityStamp = stamp, cancellationToken);
        }

        public async Task<string> GetSecurityStampAsync(CustomIdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.SecurityStamp, cancellationToken);
        }

        public async Task SetEmailAsync(CustomIdentityUser user, string email, CancellationToken cancellationToken)
        {
            await Task.Run(() => user.Email = email, cancellationToken);
        }

        public async Task<string> GetEmailAsync(CustomIdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.Email, cancellationToken);
        }

        public async Task<bool> GetEmailConfirmedAsync(CustomIdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.EmailConfirmed, cancellationToken);
        }

        public async Task SetEmailConfirmedAsync(CustomIdentityUser user, bool confirmed, CancellationToken cancellationToken)
        {
            await Task.Run(() => user.EmailConfirmed = confirmed, cancellationToken);
        }

        public async Task<CustomIdentityUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return await _userCollection.AsQueryable().Where(u => u.NormalizedEmail == normalizedEmail)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<string> GetNormalizedEmailAsync(CustomIdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.NormalizedEmail, cancellationToken);
        }

        public async Task SetNormalizedEmailAsync(CustomIdentityUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            await Task.Run(() => user.NormalizedEmail = normalizedEmail, cancellationToken);
        }

        public async Task<IList<Claim>> GetClaimsAsync(CustomIdentityUser user, CancellationToken cancellationToken)
        {
            if (user.Claims == null) user.Claims = new List<CustomIdentityUserClaim>();

            return await Task.Run(() => user.Claims?.Select(c => new Claim(c.Type, c.Value, c.ValueType, c.Issuer)).ToList(), cancellationToken);
        }

        public async Task AddClaimsAsync(CustomIdentityUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                if (user.Claims == null) user.Claims = new List<CustomIdentityUserClaim>();

                user.Claims.AddRange(claims.Select(c=> new CustomIdentityUserClaim
                {
                    Issuer = c.Issuer,
                    OriginalIssuer = c.OriginalIssuer,
                    Type = c.Type,
                    Value = c.Value,
                    ValueType = c.ValueType
                }));

            }, cancellationToken);
        }

        public async Task ReplaceClaimAsync(CustomIdentityUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                if (user.Claims == null) return;

                CustomIdentityUserClaim identityUserClaim = user.Claims.FirstOrDefault(c =>
                    c.Issuer == claim.Issuer && c.Type == claim.Type && c.Value == claim.Value &&
                    c.ValueType == claim.ValueType && c.OriginalIssuer == claim.OriginalIssuer);
                if (identityUserClaim == null) throw new NullReferenceException("IdentityUserClaim is null at ReplaceClaimAsync!");

                user.Claims.Remove(identityUserClaim);
                user.Claims.Add(new CustomIdentityUserClaim
                {
                    Issuer = newClaim.Issuer,
                    OriginalIssuer = newClaim.OriginalIssuer,
                    Type = newClaim.Type,
                    Value = newClaim.Value,
                    ValueType = newClaim.ValueType
                });

            }, cancellationToken);
        }

        public async Task RemoveClaimsAsync(CustomIdentityUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
                {
                    claims?.Select(c => user.Claims.RemoveAll(uc =>
                        uc.Issuer == c.Issuer && uc.OriginalIssuer == c.OriginalIssuer && uc.Type == c.Type &&
                        uc.Value == c.Value && uc.ValueType == c.ValueType));
                }, cancellationToken);
        }

        public async Task<IList<CustomIdentityUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            return await _userCollection.AsQueryable().Where(u => u.Claims.Any(c =>
                    c.Issuer == claim.Issuer && c.Type == claim.Type && c.Value == claim.Value &&
                    c.ValueType == claim.ValueType && c.OriginalIssuer == claim.OriginalIssuer))
                .ToListAsync(cancellationToken);
        }


        public async Task<DateTimeOffset?> GetLockoutEndDateAsync(CustomIdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.LockoutEndDateUtc, cancellationToken);
        }

        public async Task SetLockoutEndDateAsync(CustomIdentityUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            await Task.Run(() => user.LockoutEndDateUtc = lockoutEnd, cancellationToken);
        }

        public async Task<int> IncrementAccessFailedCountAsync(CustomIdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.AccessFailedCount++, cancellationToken);
        }

        public async Task ResetAccessFailedCountAsync(CustomIdentityUser user, CancellationToken cancellationToken)
        {
            await Task.Run(() => user.AccessFailedCount = 0, cancellationToken);
        }

        public async Task<int> GetAccessFailedCountAsync(CustomIdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.AccessFailedCount, cancellationToken);
        }

        public async Task<bool> GetLockoutEnabledAsync(CustomIdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.LockoutEnabled, cancellationToken);
        }

        public async Task SetLockoutEnabledAsync(CustomIdentityUser user, bool enabled, CancellationToken cancellationToken)
        {
            await Task.Run(() => user.LockoutEnabled = enabled, cancellationToken);
        }

        public async Task SetTokenAsync(CustomIdentityUser user, string loginProvider, string name, string value,
            CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                if (user.Tokens == null) user.Tokens = new List<CustomIdentityUserToken>();

                CustomIdentityUserToken identityUserToken =
                    user.Tokens.FirstOrDefault(t => t.LoginProvider == loginProvider && t.Name == name);

                if (identityUserToken != null)
                {
                    identityUserToken.Value = value;
                    return;
                }

                user.Tokens.Add(new CustomIdentityUserToken
                {
                    LoginProvider = loginProvider,
                    Name = name,
                    Value = value
                });
            }, cancellationToken);
        }

        public async Task RemoveTokenAsync(CustomIdentityUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            await Task.Run(() => user.Tokens.RemoveAll(t => t.LoginProvider == loginProvider && t.Name == name),
                cancellationToken);
        }

        public async Task<string> GetTokenAsync(CustomIdentityUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
                user.Tokens.FirstOrDefault(t => t.LoginProvider == loginProvider && t.Name == name).Value, cancellationToken);
        }

        IQueryable<CustomIdentityUser> IQueryableUserStore<CustomIdentityUser>.Users => _userCollection.AsQueryable();
    }
}
