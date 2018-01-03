using System;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Extentions.Filters.Models;
using MongoDB.Bson;
using MongoDB.Driver.Linq;

namespace HomeApp.Core.Extentions.Filters
{
    /// <summary>
    /// Расширения для объявлений
    /// </summary>
    public static class RealEstateFilterExt
    {
        /// <summary>
        /// Фильтрует объявления
        /// </summary>
        /// <param name="realEstates">Объявления</param>
        /// <param name="filter">Фильтр</param>
        /// <returns></returns>
        public static IMongoQueryable<RealEstate> Filter(this IMongoQueryable<RealEstate> realEstates, RealEstateFilter filter)
        {
            if (filter == null) throw new InvalidCastException("Filter must be cast to RealEstateFilter");

            if (filter.RealEstateStatusList?.Count > 0)
            {
                realEstates = realEstates.Where(r => filter.RealEstateStatusList.Contains(r.RealEstateStatus));
            }

            if (filter.PublishDate != null)
            {
                realEstates = realEstates.Where(r => r.PublishDate >= filter.PublishDate.Low && r.PublishDate <= filter.PublishDate.Hight);
            }
            if (filter.UserId != ObjectId.Empty)
            {
                realEstates = realEstates.Where(r => r.UserId == filter.UserId);
            }

            return realEstates;
        }

    }
}