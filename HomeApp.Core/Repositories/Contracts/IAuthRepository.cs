using System.Threading.Tasks;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Db.Entities.Models.Enums;

namespace HomeApp.Core.Repositories.Contracts
{
    /// <summary>
    /// Авторизация и регистрация пользователей
    /// </summary>
    public interface IAuthRepository
    {
        /// <summary>
        /// Вход пользователя в систему
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <returns></returns>
        Task<Session> LogIn(string login, string password);
        /// <summary>
        /// Выход из системы
        /// </summary>
        /// <param name="session">Сессия</param>
        /// <returns></returns>
        Task LogOut(Session session);
        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="email">Электронная почта</param>
        /// <param name="password">Пароль</param>
        /// <param name="confirmPass">Повторный ввод пароля</param>
        /// <param name="lan">Язык</param>
        /// <returns></returns>
        Task Register(string email, string password, string confirmPass, Language lan);
        /// <summary>
        /// Подтверждение пользователя при регистрации
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="activeCode">Код активации учетной записи</param>
        /// <returns></returns>
        Task<Session> ConfirmUser(string userId, string activeCode);
    }
}
