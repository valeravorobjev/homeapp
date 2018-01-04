using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HomeApp.Core.Db;
using Microsoft.AspNetCore.Identity;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace HomeApp.Core.Identity.CustomProvider
{
    public class CustomRoleStore : IQueryableRoleStore<CustomIdentityRole>
    {
        private readonly IMongoCollection<CustomIdentityRole> _roleCollection;

        public CustomRoleStore(string con)
        {
            IMongoClient client = new MongoClient(con);
            IMongoDatabase db = client.GetDatabase(DbSet.DB_NAME_FAKE); // TODO:: fake!!!

            _roleCollection = db.GetCollection<CustomIdentityRole>(DbSet.IDENTITY_ROLES_COLLECTION);
        }


        public void Dispose()
        {
        }

        public async Task<IdentityResult> CreateAsync(CustomIdentityRole role, CancellationToken cancellationToken)
        {
            try
            {
                await _roleCollection.InsertOneAsync(role, cancellationToken: cancellationToken);
            }
            catch
            {
                return IdentityResult.Failed();
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(CustomIdentityRole role, CancellationToken cancellationToken)
        {
            try
            {
                await _roleCollection.ReplaceOneAsync(r => r.Id == role.Id, role, cancellationToken: cancellationToken);
            }
            catch
            {
                return IdentityResult.Failed();
            }

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(CustomIdentityRole role, CancellationToken cancellationToken)
        {
            try
            {
                await _roleCollection.DeleteOneAsync(r => r.Id == role.Id, cancellationToken);
            }
            catch
            {
                return IdentityResult.Failed();
            }

            return IdentityResult.Success;
        }

        public async Task<string> GetRoleIdAsync(CustomIdentityRole role, CancellationToken cancellationToken)
        {
            return await Task.Run(() => role.Id.ToString(), cancellationToken);
        }
            
        

        public async Task<string> GetRoleNameAsync(CustomIdentityRole role, CancellationToken cancellationToken)
        {
            return await Task.Run(() => role.Name, cancellationToken);
        }

        public async Task SetRoleNameAsync(CustomIdentityRole role, string roleName, CancellationToken cancellationToken)
        {
            await Task.Run(() => { role.Name = roleName; }, cancellationToken);
        }

        public async Task<string> GetNormalizedRoleNameAsync(CustomIdentityRole role, CancellationToken cancellationToken)
        {
            return await Task.Run(() => role.NormalizedName, cancellationToken);
        }

        public async Task SetNormalizedRoleNameAsync(CustomIdentityRole role, string normalizedName, CancellationToken cancellationToken)
        {
            await Task.Run(() => { role.NormalizedName = normalizedName; }, cancellationToken);
        }

        public async Task<CustomIdentityRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return await _roleCollection.AsQueryable().FirstOrDefaultAsync(r => r.Id == ObjectId.Parse(roleId), cancellationToken);
        }

        public async Task<CustomIdentityRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return await _roleCollection.AsQueryable().FirstOrDefaultAsync(r => r.NormalizedName == normalizedRoleName, cancellationToken);
        }

        public IQueryable<CustomIdentityRole> Roles => _roleCollection.AsQueryable();
    }
}
