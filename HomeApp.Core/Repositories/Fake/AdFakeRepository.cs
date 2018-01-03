using System.Threading.Tasks;
using HomeApp.Core.Db.Entities.Models;
using HomeApp.Core.Repositories.Contracts;
using MongoDB.Bson;

namespace HomeApp.Core.Repositories.Fake
{
    /// <summary>
    /// Фейковый репозиторий для рекламной компании
    /// </summary>
    public class AdFakeRepository : IAdRepository
    {
        Task<UserAd> IAdRepository.GetAdForUser(ObjectId userId)
        {
            return Task.Run(() =>
                new UserAd());
        }
    }
}
