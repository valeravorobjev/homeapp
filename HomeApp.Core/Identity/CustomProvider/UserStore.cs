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
    public class UserStore : IUserPasswordStore<IdentityUser>,
        IUserRoleStore<IdentityUser>,
        IUserLoginStore<IdentityUser>,
        IUserSecurityStampStore<IdentityUser>,
        IUserEmailStore<IdentityUser>,
        IUserClaimStore<IdentityUser>,
        IUserLockoutStore<IdentityUser>,
        IQueryableUserStore<IdentityUser>,
        IUserAuthenticationTokenStore<IdentityUser>
    {
        private readonly IMongoCollection<IdentityUser> _userCollection;

        public UserStore(string con)
        {
            IMongoClient client = new MongoClient(con);
            IMongoDatabase db = client.GetDatabase(DbSet.DB_NAME_FAKE); // TODO:: fake!!!

            _userCollection = db.GetCollection<IdentityUser>(DbSet.IDENTITY_USERS_COLLECTION);
        }

        public void Dispose()
        {
        }

        public async Task<string> GetUserIdAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.Id.ToString(), cancellationToken);
        }

        public async Task<string> GetUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.Name, cancellationToken);
        }

        public async Task SetUserNameAsync(IdentityUser user, string userName, CancellationToken cancellationToken)
        {
            await Task.Run(() => user.Name = userName, cancellationToken);
        }

        public async Task<string> GetNormalizedUserNameAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.NormalizedUserName, cancellationToken);
        }

        public async Task SetNormalizedUserNameAsync(IdentityUser user, string normalizedName, CancellationToken cancellationToken)
        {
            await Task.Run(() => user.NormalizedUserName = normalizedName, cancellationToken);
        }

        public async Task<IdentityResult> CreateAsync(IdentityUser user, CancellationToken cancellationToken)
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

        public async Task<IdentityResult> UpdateAsync(IdentityUser user, CancellationToken cancellationToken)
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

        public async Task<IdentityResult> DeleteAsync(IdentityUser user, CancellationToken cancellationToken)
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

        public async Task<IdentityUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            return await _userCollection.AsQueryable().FirstOrDefaultAsync(u => u.Id == ObjectId.Parse(userId), cancellationToken: cancellationToken);
        }

        public async Task<IdentityUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await _userCollection.AsQueryable().FirstOrDefaultAsync(u => u.NormalizedUserName == normalizedUserName, cancellationToken: cancellationToken);
        }

        public async Task SetPasswordHashAsync(IdentityUser user, string passwordHash, CancellationToken cancellationToken)
        {
            await Task.Run(() => user.PasswordHash = passwordHash, cancellationToken);
        }

        public async Task<string> GetPasswordHashAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.PasswordHash, cancellationToken);
        }

        public async Task<bool> HasPasswordAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
             {
                 bool hasPwd = !string.IsNullOrEmpty(user.PasswordHash);
                 return hasPwd;

             }, cancellationToken);
        }

        public async Task AddToRoleAsync(IdentityUser user, string roleName, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                if (user.Roles == null) user.Roles = new List<string>();
                user.Roles.Add(roleName);
            }, cancellationToken);
        }

        public async Task RemoveFromRoleAsync(IdentityUser user, string roleName, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                if (user.Roles == null) return;
                user.Roles.Remove(roleName);
            }, cancellationToken);
        }

        public async Task<IList<string>> GetRolesAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.Roles, cancellationToken);
        }

        public async Task<bool> IsInRoleAsync(IdentityUser user, string roleName, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.Roles.Contains(roleName), cancellationToken);
        }

        public async Task<IList<IdentityUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            return await _userCollection.AsQueryable().Where(u => u.Roles.Contains(roleName))
                .ToListAsync(cancellationToken);
        }

        public async Task AddLoginAsync(IdentityUser user, UserLoginInfo login, CancellationToken cancellationToken)
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

        public async Task RemoveLoginAsync(IdentityUser user, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                if (user.Logins == null) return;
                user.Logins.RemoveAll(l => l.LoginProvider == loginProvider && l.ProviderKey == providerKey);

            }, cancellationToken);
        }

        public async Task<IList<UserLoginInfo>> GetLoginsAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.Logins.Select(l => new UserLoginInfo(l.LoginProvider, l.ProviderKey, l.ProviderDisplayName)).ToList(), cancellationToken);
        }

        public async Task<IdentityUser> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            return await _userCollection.AsQueryable()
                .Where(u => u.Logins.Any(l => l.LoginProvider == loginProvider && l.ProviderKey == providerKey))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task SetSecurityStampAsync(IdentityUser user, string stamp, CancellationToken cancellationToken)
        {
            await Task.Run(() => user.SecurityStamp = stamp, cancellationToken);
        }

        public async Task<string> GetSecurityStampAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.SecurityStamp, cancellationToken);
        }

        public async Task SetEmailAsync(IdentityUser user, string email, CancellationToken cancellationToken)
        {
            await Task.Run(() => user.Email = email, cancellationToken);
        }

        public async Task<string> GetEmailAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.Email, cancellationToken);
        }

        public async Task<bool> GetEmailConfirmedAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.EmailConfirmed, cancellationToken);
        }

        public async Task SetEmailConfirmedAsync(IdentityUser user, bool confirmed, CancellationToken cancellationToken)
        {
            await Task.Run(() => user.EmailConfirmed = confirmed, cancellationToken);
        }

        public async Task<IdentityUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return await _userCollection.AsQueryable().Where(u => u.NormalizedEmail == normalizedEmail)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<string> GetNormalizedEmailAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.NormalizedEmail, cancellationToken);
        }

        public async Task SetNormalizedEmailAsync(IdentityUser user, string normalizedEmail, CancellationToken cancellationToken)
        {
            await Task.Run(() => user.NormalizedEmail = normalizedEmail, cancellationToken);
        }

        public async Task<IList<Claim>> GetClaimsAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.Claims.Select(c => new Claim(c.Type, c.Value, c.ValueType, c.Issuer)).ToList(), cancellationToken);
        }

        public async Task AddClaimsAsync(IdentityUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                if (user.Claims == null) user.Claims = new List<IdentityUserClaim>();

                user.Claims.AddRange(claims.Select(c=> new IdentityUserClaim
                {
                    Issuer = c.Issuer,
                    OriginalIssuer = c.OriginalIssuer,
                    Type = c.Type,
                    Value = c.Value,
                    ValueType = c.ValueType
                }));

            }, cancellationToken);
        }

        public async Task ReplaceClaimAsync(IdentityUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                if (user.Claims == null) return;

                IdentityUserClaim identityUserClaim = user.Claims.FirstOrDefault(c =>
                    c.Issuer == claim.Issuer && c.Type == claim.Type && c.Value == claim.Value &&
                    c.ValueType == claim.ValueType && c.OriginalIssuer == claim.OriginalIssuer);
                if (identityUserClaim == null) throw new NullReferenceException("IdentityUserClaim is null at ReplaceClaimAsync!");

                user.Claims.Remove(identityUserClaim);
                user.Claims.Add(new IdentityUserClaim
                {
                    Issuer = newClaim.Issuer,
                    OriginalIssuer = newClaim.OriginalIssuer,
                    Type = newClaim.Type,
                    Value = newClaim.Value,
                    ValueType = newClaim.ValueType
                });

            }, cancellationToken);
        }

        public async Task RemoveClaimsAsync(IdentityUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            await Task.Run(() =>
                {
                    claims.Select(c => user.Claims.RemoveAll(uc =>
                        uc.Issuer == c.Issuer && uc.OriginalIssuer == c.OriginalIssuer && uc.Type == c.Type &&
                        uc.Value == c.Value && uc.ValueType == c.ValueType));
                }, cancellationToken);
        }

        public async Task<IList<IdentityUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            return await _userCollection.AsQueryable().Where(u => u.Claims.Any(c =>
                    c.Issuer == claim.Issuer && c.Type == claim.Type && c.Value == claim.Value &&
                    c.ValueType == claim.ValueType && c.OriginalIssuer == claim.OriginalIssuer))
                .ToListAsync(cancellationToken);
        }


        public async Task<DateTimeOffset?> GetLockoutEndDateAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.LockoutEndDateUtc, cancellationToken);
        }

        public async Task SetLockoutEndDateAsync(IdentityUser user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
        {
            await Task.Run(() => user.LockoutEndDateUtc = lockoutEnd, cancellationToken);
        }

        public async Task<int> IncrementAccessFailedCountAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.AccessFailedCount++, cancellationToken);
        }

        public async Task ResetAccessFailedCountAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            await Task.Run(() => user.AccessFailedCount = 0, cancellationToken);
        }

        public async Task<int> GetAccessFailedCountAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.AccessFailedCount, cancellationToken);
        }

        public async Task<bool> GetLockoutEnabledAsync(IdentityUser user, CancellationToken cancellationToken)
        {
            return await Task.Run(() => user.LockoutEnabled, cancellationToken);
        }

        public async Task SetLockoutEnabledAsync(IdentityUser user, bool enabled, CancellationToken cancellationToken)
        {
            await Task.Run(() => user.LockoutEnabled = enabled, cancellationToken);
        }

        public async Task SetTokenAsync(IdentityUser user, string loginProvider, string name, string value,
            CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                if (user.Tokens == null) user.Tokens = new List<IdentityUserToken>();

                IdentityUserToken identityUserToken =
                    user.Tokens.FirstOrDefault(t => t.LoginProvider == loginProvider && t.Name == name);

                if (identityUserToken != null)
                {
                    identityUserToken.Value = value;
                    return;
                }

                user.Tokens.Add(new IdentityUserToken
                {
                    LoginProvider = loginProvider,
                    Name = name,
                    Value = value
                });
            }, cancellationToken);
        }

        public async Task RemoveTokenAsync(IdentityUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            await Task.Run(() => user.Tokens.RemoveAll(t => t.LoginProvider == loginProvider && t.Name == name),
                cancellationToken);
        }

        public async Task<string> GetTokenAsync(IdentityUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            return await Task.Run(() =>
                user.Tokens.FirstOrDefault(t => t.LoginProvider == loginProvider && t.Name == name).Value, cancellationToken);
        }

        IQueryable<IdentityUser> IQueryableUserStore<IdentityUser>.Users => _userCollection.AsQueryable();
    }
}
