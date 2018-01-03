using System;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Exceptions;
using HomeApp.Core.Extentions.Filters.Models;
using MongoDB.Driver.Linq;

namespace HomeApp.Core.Extentions.Filters
{
    /// <summary>
    /// Расширения для пользователей
    /// </summary>
    public static class UserFilterExt
    {
        /// <summary>
        /// Фильтрует пользователей
        /// </summary>
        /// <param name="users">Коллекция пользователей</param>
        /// <param name="filter">Фильтр</param>
        /// <returns></returns>
        public static IMongoQueryable<User> Filter(this IMongoQueryable<User> users, UserFilter filter)
        {
            if (filter == null) throw new InvalidCastException("Filter must be cast to UserFilter");

            if (filter.UserModes?.Count > 0)
            {
                users = users.Where(u => filter.UserModes.Contains(u.UserMode));
            }

            if (filter.UserTypes?.Count > 0)
            {
                users = users.Where(u =>filter.UserTypes.Contains(u.UserType));
            }

            if (filter.Address != null)
            {
                if (filter.Address.Country != null)
                {
                    string country = filter.Address.Country.En ?? filter.Address.Country.Ru;
                    if (string.IsNullOrEmpty(country)) throw new AddressException("Contry name is null or empty");
                    users = users.Where(r => r.Address.Country.En == country || r.Address.Country.Ru == country);
                }
                if (filter.Address.Region != null)
                {
                    string region = filter.Address.Region.En ?? filter.Address.Region.Ru;
                    if (string.IsNullOrEmpty(region)) throw new AddressException("Region name is null or empty");
                    users = users.Where(r => r.Address.Region.En == region || r.Address.Region.Ru == region);
                }
                if (filter.Address.Area != null)
                {
                    string area = filter.Address.Area.En ?? filter.Address.Area.Ru;
                    if (string.IsNullOrEmpty(area)) throw new AddressException("Area name is null or empty");
                    users = users.Where(r => r.Address.Area.En == area || r.Address.Area.Ru == area);
                }
                if (filter.Address.Sity != null)
                {
                    string sity = filter.Address.Sity.En ?? filter.Address.Sity.Ru;
                    if (string.IsNullOrEmpty(sity)) throw new AddressException("Sity name is null or empty");
                    users = users.Where(r => r.Address.Sity.En == sity || r.Address.Sity.Ru == sity);
                }
                if (filter.Address.Street != null)
                {
                    string street = filter.Address.Street.En ?? filter.Address.Street.Ru;
                    if (string.IsNullOrEmpty(street)) throw new AddressException("Street name is null or empty");
                    users = users.Where(r => r.Address.Street.En == street || r.Address.Street.Ru == street);
                }
                if (filter.Address.StreetNumber != null)
                {
                    string streetNumber = filter.Address.StreetNumber.En ?? filter.Address.StreetNumber.Ru;
                    if (string.IsNullOrEmpty(streetNumber)) throw new AddressException("StreetNumber name is null or empty");
                    users = users.Where(r => r.Address.StreetNumber.En == streetNumber || r.Address.StreetNumber.Ru == streetNumber);
                }
                if (filter.Address.Metro != null)
                {
                    string metro = filter.Address.Metro.En ?? filter.Address.Metro.Ru;
                    if (string.IsNullOrEmpty(metro)) throw new AddressException("Metro name is null or empty");
                    users = users.Where(r => r.Address.Metro.En == metro || r.Address.Metro.Ru == metro);
                }
                if (filter.Address.MetroTime > 0)
                {
                    users = users.Where(r => r.Address.MetroTime == filter.Address.MetroTime);
                }
            }

            return users;
        }
    }
}
