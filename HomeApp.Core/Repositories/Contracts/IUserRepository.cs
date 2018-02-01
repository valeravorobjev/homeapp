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
        /// <param name="realtor">Проф. данные пользователя</param>
        Task SetRealtorAsync(ObjectId userId, Realtor realtor);
        
        /// <summary>
        /// Устанавливает или обновляет социальные сети пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="socialMedia">Социальные сети</param>
        /// <returns></returns>
        Task SetSocialMediaAsync(ObjectId userId, SocialMedia socialMedia);

        /// <summary>
        /// Добавляет фотограффию пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="contentType">Тип файла</param>
        /// <param name="serverPath">Путь к дирректории wwwroot на сервере</param>
        /// <param name="fileName">Имя файла</param>
        /// <param name="file">Файл</param>
        /// <returns></returns>
        Task SetPhotoAsync(ObjectId userId, string contentType, string serverPath, string fileName, byte[] file);

        /// <summary>
        /// Удаляет аватар пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="serverPath">Путь к серверной папке wwwroot</param>
        /// <returns></returns>
        Task DeletePhotoAsync(ObjectId userId, string serverPath);

        /// <summary>
        /// Делает пользователя активным
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns></returns>
        Task MakeUserActiveAsync(ObjectId userId);
    }
}
