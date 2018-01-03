using System.Collections.Generic;
using System.Threading.Tasks;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Db.Entities.Models.Enums;
using HomeApp.Core.Extentions.Filters.Models;
using HomeApp.Core.Extentions.Sorts.Models.Enums;
using HomeApp.Core.ViewModels;
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
        Task<User> GetUser(ObjectId userId);
        /// <summary>
        /// Возвращает список пользователей
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <param name="sortType">Сортировка</param>
        /// <returns></returns>
        Task<UserList> GetUsers(UserFilter filter, UserSort sortType);
        /// <summary>
        /// Возвращает топ пользователей
        /// </summary>
        /// <param name="userTypes">Типы пользователей</param>
        /// <param name="take">Сколько записей взять</param>
        /// <returns></returns>
        Task<UserList> GetTopUsers(List<UserType> userTypes, int take);
        /// <summary>
        /// Возвращает топ пользователей, с количеством размещенных, активных объявлений
        /// </summary>
        /// <param name="userTypes"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        Task<UserWithRealEstateCountList> GetTopUsersWithRealEstateCount(List<UserType> userTypes, int take);
        /// <summary>
        /// Возвращает список профессионалов (агенства, застройщики)
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <param name="sortType">Сортировка</param>
        /// <returns></returns>
        Task<UserList> GetProfessionals(ProfessionalFilter filter, ProfessionalSort sortType);
        /// <summary>
        /// Возвращает список профессионалов (риэлторы)
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <param name="sortType">Сортировка</param>
        /// <returns></returns>
        Task<UserList> GetPersonProfessionals(PersonProfessionalFilter filter, PersonProfessionalSort sortType);
        /// <summary>
        /// Возвращает список комментариев для пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="skip">Сколько записей пропустить</param>
        /// <param name="take">Сколько записей взять</param>
        /// <returns></returns>
        Task<CommentList> GetComments(ObjectId userId, int skip, int take);
    }
}
