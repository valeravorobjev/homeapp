using System.Threading.Tasks;
using HomeApp.Core.Db.Entities;
using MongoDB.Bson;

namespace HomeApp.Core.Repositories.Contracts
{
    /// <summary>
    /// Работа с сессией
    /// </summary>
    public interface ISessionRepository
    {
        /// <summary>
        /// Идентификатор сессии
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        Task<Session> GetSession(ObjectId sessionId);
        /// <summary>
        /// Изменить токен
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="token">Ключ</param>
        /// <returns></returns>
        Task ChangeToken(ObjectId userId, string token);
        /// <summary>
        /// Открыть сессию
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        Task<Session> OpenSession(User user);
        /// <summary>
        /// Закрыть сессию
        /// </summary>
        /// <param name="sessionId">Идентификатор сессии</param>
        /// <returns></returns>
        Task CloseSession(ObjectId sessionId);
    }
}
