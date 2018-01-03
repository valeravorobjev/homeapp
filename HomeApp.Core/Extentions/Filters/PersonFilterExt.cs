using System;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Db.Entities.Models;
using HomeApp.Core.Extentions.Filters.Models;
using MongoDB.Driver.Linq;

namespace HomeApp.Core.Extentions.Filters
{
    /// <summary>
    /// Расшинения для персон
    /// </summary>
    public static class PersonFilterExt
    {
        /// <summary>
        /// Фильтрует физических пользователей (персон)
        /// </summary>
        /// <param name="users">Пользователи</param>
        /// <param name="filter">Фильтр</param>
        /// <returns></returns>
        public static IMongoQueryable<User> Filter(this IMongoQueryable<User> users, PersonFilter filter)
        {
            if (filter == null) throw new InvalidCastException("Filter must be cast to UserFilter");
            users = users.Filter((UserFilter) filter);

            if (filter.DateBirth != null)
            {
                users = users.Where(u => ((Person) u).DateBirth == filter.DateBirth);
            }
            if (filter.FirstName != null)
            {
                if (!string.IsNullOrEmpty(filter.FirstName.Ru))
                    users = users.Where(u => ((Person)u).FirstName.Ru == filter.FirstName.Ru);
                else if (!string.IsNullOrEmpty(filter.FirstName.En))
                    users = users.Where(u => ((Person) u).FirstName.En == filter.FirstName.En);
            }
            if (filter.LastName != null)
            {
                if (!string.IsNullOrEmpty(filter.LastName.Ru))
                    users = users.Where(u => ((Person)u).LastName.Ru == filter.LastName.Ru);
                else if (!string.IsNullOrEmpty(filter.LastName.En))
                    users = users.Where(u => ((Person)u).LastName.En == filter.LastName.En);
            }
            if (filter.MidName != null)
            {
                if (!string.IsNullOrEmpty(filter.MidName.Ru))
                    users = users.Where(u => ((Person)u).MidName.Ru == filter.MidName.Ru);
                else if (!string.IsNullOrEmpty(filter.MidName.En))
                    users = users.Where(u => ((Person)u).MidName.En == filter.MidName.En);
            }
            if (filter.DateBirth != null)
            {
                users = users.Where(u => ((Person) u).DateBirth == filter.DateBirth);
            }
            if (filter.Sex != null)
            {
                users = users.Where(u => ((Person) u).Sex == filter.Sex);
            }

            return users;
        }
    }
}
