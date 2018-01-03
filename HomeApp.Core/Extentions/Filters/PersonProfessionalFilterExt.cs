using System;
using System.Linq;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Db.Entities.Models;
using HomeApp.Core.Extentions.Filters.Models;
using MongoDB.Driver.Linq;

namespace HomeApp.Core.Extentions.Filters
{
    /// <summary>
    /// Расширения для пользователей
    /// </summary>
    public static class PersonProfessionalFilterExt
    {
        /// <summary>
        /// Фильтрует пользователей профессионалов
        /// </summary>
        /// <param name="users">Пользователи</param>
        /// <param name="filter">Фильтр</param>
        /// <returns></returns>
        public static IMongoQueryable<User> Filter(this IMongoQueryable<User> users, PersonProfessionalFilter filter)
        {
            if (filter == null) throw new InvalidCastException("Filter must be cast to PersonProfessionalFilter");
            users = users.Filter((PersonFilter)filter);

            if (filter.FromYear != null)
            {
                users =
                    users.Where(
                        u =>
                            ((PersonProfessional)u).FromYear >= filter.FromYear.Low &&
                            ((PersonProfessional)u).FromYear >= filter.FromYear.Hight);
            }
            if (filter.WorkRegions?.Count > 0)
            {
                users = users.Where(u => ((PersonProfessional) u).WorkRegions.Any(w => filter.WorkRegions.Contains(w)));
            }
            if (filter.Specialization?.EstateSales?.Count > 0)
            {
                users =
                    users.Where(
                        u =>
                            ((PersonProfessional) u).Specialization.EstateSales.Any(
                                es => filter.Specialization.EstateSales.Contains(es)));
            }
            if (filter.Specialization?.RentalProperties?.Count > 0)
            {
                users =
                    users.Where(
                        u =>
                            ((PersonProfessional)u).Specialization.RentalProperties.Any(
                                es => filter.Specialization.RentalProperties.Contains(es)));
            }
            if (filter.Specialization?.AddServices?.Count > 0)
            {
                users =
                    users.Where(
                        u =>
                            ((PersonProfessional)u).Specialization.AddServices.Any(
                                es => filter.Specialization.AddServices.Contains(es)));
            }
            if (!string.IsNullOrEmpty(filter.Site))
            {
                users = users.Where(u => ((PersonProfessional) u).Site == filter.Site);
            }

            //if (filter.Name != null)
            //{
            //    if (!string.IsNullOrEmpty(filter.Name.Ru))
            //        users = users.Where(u => ((PersonProfessional)u).Name.Ru == filter.Name.Ru);
            //    else if (!string.IsNullOrEmpty(filter.Name.En))
            //        users = users.Where(u => ((PersonProfessional)u).Name.En == filter.Name.En);
            //}

            return users;
        }
    }
}
