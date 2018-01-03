using System.Threading.Tasks;
using HomeApp.Core.Db.Entities.Models;
using MongoDB.Bson;

namespace HomeApp.Core.Repositories.Contracts
{
    /// <summary>
    /// Работа с рекламными компаниями
    /// </summary>
    public interface IAdRepository
    {
        /// <summary>
        /// Возвращает рекламную компанию для пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        Task<UserAd> GetAdForUser(ObjectId userId);
    }
}
