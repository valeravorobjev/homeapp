using System;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Extentions.Filters.Models;
using MongoDB.Driver.Linq;

namespace HomeApp.Core.Extentions.Filters
{
    /// <summary>
    /// Расширения для риэлторов
    /// </summary>
    public static class RialtorFilterExt
    {
        /// <summary>
        /// Фильтрует риэлторов
        /// </summary>
        /// <param name="users">Пользователи</param>
        /// <param name="filter">Фильтр</param>
        /// <returns></returns>
        public static IMongoQueryable<User> Filter(this IMongoQueryable<User> users, RialtorFilter filter)
        {
            if (filter == null) throw new InvalidCastException("Filter must be cast to RialtorFilter");

            users = users.Filter((PersonProfessionalFilter)filter);
            return users;
        }
    }
}
