using System.Threading.Tasks;
using HomeApp.Core.Db.Entities.Models;
using HomeApp.Core.Repositories.Contracts;
using MongoDB.Bson;

namespace HomeApp.Core.Repositories
{
    /// <summary>
    /// Фейковый репозиторий для рекламной компании
    /// </summary>
    public class AdRepository : IAdRepository
    {
        Task<UserAd> IAdRepository.GetAdForUser(ObjectId userId)
        {
            return Task.Run(() =>
                new UserAd());
        }
    }
}
