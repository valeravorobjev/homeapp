using System;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Extentions.Filters.Models;
using MongoDB.Driver.Linq;

namespace HomeApp.Core.Extentions.Filters
{

    /// <summary>
    /// Расширения для агенств
    /// </summary>
    public static class DeveloperFilterExt
    {
        /// <summary>
        /// Фильтрует агенства
        /// </summary>
        /// <param name="users">Пользователи</param>
        /// <param name="filter">Фильтр</param>
        /// <returns></returns>
        public static IMongoQueryable<User> Filter(this IMongoQueryable<User> users, DeveloperFilter filter)
        {
            if (filter == null) throw new InvalidCastException("Filter must be cast to DeveloperFilter");

            users = users.Filter((ProfessionalFilter)filter);
            return users;
        }
    }
}
