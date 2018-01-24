using System.Collections.Generic;
using System.Threading.Tasks;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Db.Entities.Models;
using HomeApp.Core.Db.Entities.Models.Enums;
using HomeApp.Core.Extentions.Filters.Models;
using HomeApp.Core.Extentions.Sorts.Models.Enums;
using HomeApp.Core.Models;
using MongoDB.Bson;

namespace HomeApp.Core.Repositories.Contracts
{
    /// <summary>
    /// Работа с пользователями
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Возвращает пользователя по его id
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        Task<User> GetUserAsync(ObjectId userId);
        /// <summary>
        /// Возвращает пользователя по логину
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        /// <returns></returns>
        Task<User> GetUserAsync(string login);
        /// <summary>
        /// Возвращает список пользователей
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <param name="sortType">Сортировка</param>
        /// <returns></returns>
        Task<UsersModel> GetUsersAsync(UserFilter filter, UserSort sortType);
        /// <summary>
        /// Возвращает топ пользователей
        /// </summary>
        /// <param name="userTypes">Типы пользователей</param>
        /// <param name="take">Сколько записей взять</param>
        /// <returns></returns>
        Task<UsersModel> GetTopUsersAsync(List<UserType> userTypes, int take);
        /// <summary>
        /// Возвращает топ пользователей, с количеством размещенных, активных объявлений
        /// </summary>
        /// <param name="userTypes"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<UsersWithRealEstateCountModel> GetTopUsersWithRealEstateCountAsync(List<UserType> userTypes, int take);
        /// <summary>
        /// Возвращает список профессионалов (агенства, застройщики)
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <param name="sortType">Сортировка</param>
        /// <returns></returns>
        Task<UsersModel> GetProfessionalsAsync(ProfessionalFilter filter, ProfessionalSort sortType);
        /// <summary>
        /// Возвращает список профессионалов (риэлторы)
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <param name="sortType">Сортировка</param>
        /// <returns></returns>
        Task<UsersModel> GetPersonProfessionalsAsync(PersonProfessionalFilter filter, PersonProfessionalSort sortType);
        /// <summary>
        /// Возвращает список комментариев для пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="skip">Сколько записей пропустить</param>
        /// <param name="take">Сколько записей взять</param>
        /// <returns></returns>
        Task<CommentsModel> GetCommentsAsync(ObjectId userId, int skip, int take);

        /// <summary>
        /// Установить тип пользователя. Меняет свойство UserType а так же сохраняет в базу новый объект, унаследованный от user.
        /// Например, если тип Prson то создается класс Person.
        /// </summary>
        /// <param name="userId">Id пользователя</param>
        /// <param name="userType">Тип пользователя</param>
        /// <returns></returns>
        Task SetUserTypeAsync(ObjectId userId, UserType userType);

        /// <summary>
        /// Установить базовые данные пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="person">Профайл пользователя</param>
        /// <returns></returns>
        Task SetPersonAsync(ObjectId userId ,Person person);

        /// <summary>
        /// Добавляет профессиональную информацию о пользователе
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="personProfessional">Проф. данные пользователя</param>
        Task SetPersonProfessionalAsync(ObjectId userId, PersonProfessional personProfessional);
    }
}
