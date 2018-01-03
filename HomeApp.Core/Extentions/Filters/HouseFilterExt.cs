using System;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Extentions.Filters.Models;
using MongoDB.Driver.Linq;

namespace HomeApp.Core.Extentions.Filters
{
    /// <summary>
    /// Методы расширений для домов
    /// </summary>
    public static class HouseFilterExt
    {
        /// <summary>
        /// Фильтрация домов
        /// </summary>
        /// <param name="realEstates">Объявления</param>
        /// <param name="filter">Фильтр</param>
        /// <returns></returns>
        public static IMongoQueryable<RealEstate> Filter(this IMongoQueryable<RealEstate> realEstates, HouseFilter filter)
        {
            if (filter == null) throw new InvalidCastException("bedSpaceFilter must be cast to FlatFilter");

            realEstates = realEstates.Filter((FlatFilter)filter);

            return realEstates;
        }
    }
}
