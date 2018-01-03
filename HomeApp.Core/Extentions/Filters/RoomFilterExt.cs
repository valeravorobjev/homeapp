using System;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Db.Entities.Models;
using HomeApp.Core.Extentions.Filters.Models;
using MongoDB.Driver.Linq;

namespace HomeApp.Core.Extentions.Filters
{
    /// <summary>
    /// Методы расширений для комнат
    /// </summary>
    public static class RoomFilterExt
    {
        /// <summary>
        /// Фильтрует комнаты
        /// </summary>
        /// <param name="realEstates">Объявления</param>
        /// <param name="filter">Фильтр</param>
        /// <returns></returns>
        public static IMongoQueryable<RealEstate> Filter(this IMongoQueryable<RealEstate> realEstates, RoomFilter filter)
        {
            if (filter == null) throw new InvalidCastException("bedSpaceFilter must be cast to RoomFilter");

            realEstates = realEstates.Filter((BedSpaceFilter)filter);
            if (filter.RoomCounts?.Count > 0)
            {
                realEstates = realEstates.Where(r => filter.RoomCounts.Contains(((Room)r.Unit).RoomCount));
            }
            if (filter.HouseTypes != null)
            {
                realEstates = realEstates.Where(r => filter.HouseTypes.Contains(((Room)r.Unit).HouseType));
            }
            if (filter.Pledge != null)
            {
                realEstates = realEstates.Where(r => ((Room)r.Unit).Pledge >= filter.Pledge.Low && ((Room)r.Unit).Pledge <= filter.Pledge.Hight);
            }
            if (filter.PrepaymentTerm != null)
            {
                realEstates = realEstates.Where(r => ((Room)r.Unit).PrepaymentTerm == filter.PrepaymentTerm);
            }
            if (filter.UtilityPayment != null)
            {
                realEstates = realEstates.Where(r => ((Room)r.Unit).UtilityPayment == filter.UtilityPayment);
            }
            if (filter.BathroomCounts?.Count > 0)
            {
                realEstates = realEstates.Where(r => filter.BathroomCounts.Contains(((Room)r.Unit).BathroomCount));
            }
            if (filter.CombinedBathroomCounts?.Count > 0)
            {
                realEstates = realEstates.Where(r => filter.CombinedBathroomCounts.Contains(((Room)r.Unit).CombinedBathroomCount));
            }

            return realEstates;
        }
    }
}
