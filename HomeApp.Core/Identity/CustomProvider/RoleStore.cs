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
    public class RoleStore: IQueryableRoleStore<IdentityRole>
    {
        private readonly IMongoCollection<IdentityRole> _roleCollection;

        public RoleStore(string con)
        {
            IMongoClient client = new MongoClient(con);
            IMongoDatabase db = client.GetDatabase(DbSet.DB_NAME_FAKE); // TODO:: fake!!!

            _roleCollection = db.GetCollection<IdentityRole>(DbSet.IDENTITY_ROLES_COLLECTION);
        }


        public void Dispose()
        {
        }

        public async Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancellationToken)
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

        public async Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancellationToken)
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

        public async Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancellationToken)
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

        public async Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return await Task.Run(() => role.Id.ToString(), cancellationToken);
        }
            
        

        public async Task<string> GetRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return await Task.Run(() => role.Name, cancellationToken);
        }

        public async Task SetRoleNameAsync(IdentityRole role, string roleName, CancellationToken cancellationToken)
        {
            await Task.Run(() => { role.Name = roleName; }, cancellationToken);
        }

        public async Task<string> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            return await Task.Run(() => role.NormalizedName, cancellationToken);
        }

        public async Task SetNormalizedRoleNameAsync(IdentityRole role, string normalizedName, CancellationToken cancellationToken)
        {
            await Task.Run(() => { role.NormalizedName = normalizedName; }, cancellationToken);
        }

        public async Task<IdentityRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            return await _roleCollection.AsQueryable().FirstOrDefaultAsync(r => r.Id == ObjectId.Parse(roleId), cancellationToken);
        }

        public async Task<IdentityRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return await _roleCollection.AsQueryable().FirstOrDefaultAsync(r => r.NormalizedName == normalizedRoleName, cancellationToken);
        }

        public IQueryable<IdentityRole> Roles => _roleCollection.AsQueryable();
    }
}
