using System;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Db.Entities.Models;
using HomeApp.Core.Extentions.Filters.Models;
using MongoDB.Driver.Linq;

namespace HomeApp.Core.Extentions.Filters
{
    /// <summary>
    /// Методы расширений для квартир
    /// </summary>
    public static class FlatFilterExt
    {
        /// <summary>
        /// Фильтрация квартир
        /// </summary>
        /// <param name="realEstates">Объявления</param>
        /// <param name="filter">Фильтр</param>
        /// <returns></returns>
        public static IMongoQueryable<RealEstate> Filter(this IMongoQueryable<RealEstate> realEstates, FlatFilter filter)
        {
            if (filter == null) throw new InvalidCastException("bedSpaceFilter must be cast to FlatFilter");

            realEstates = realEstates.Filter((RoomFilter)filter);

            if (filter.OldTypes?.Count > 0)
            {
                realEstates = realEstates.Where(r => filter.OldTypes.Contains(((Flat)r.Unit).OldType));
            }

            return realEstates;
        }
    }
}
