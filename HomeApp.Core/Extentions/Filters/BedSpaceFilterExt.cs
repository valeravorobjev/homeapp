using System;
using System.Linq;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Db.Entities.Models;
using HomeApp.Core.Extentions.Filters.Models;
using MongoDB.Driver.Linq;

namespace HomeApp.Core.Extentions.Filters
{
    /// <summary>
    /// Методы расширений для койко мест
    /// </summary>
    public static class BedSpaceFilterExt
    {
        /// <summary>
        /// Фильтр койко мест
        /// </summary>
        /// <param name="realEstates">Объявления</param>
        /// <param name="filter">Фильтр</param>
        /// <returns></returns>
        public static IMongoQueryable<RealEstate> Filter(this IMongoQueryable<RealEstate> realEstates, BedSpaceFilter filter)
        {
            if (filter == null) throw new InvalidCastException("bedSpaceFilter must be cast to BedSpaceFilter");

            realEstates = realEstates.Filter((UnitBaseFilter) filter);

            if (filter.Floors?.Count > 0)
            {
                realEstates = realEstates.Where(b => filter.Floors.Contains(((BedSpace)b.Unit).Floor));
            }
            if (filter.KitchenArea != null)
            {
                realEstates = realEstates.Where(b => ((BedSpace)b.Unit).KitchenArea >= filter.KitchenArea.Low && ((BedSpace)b.Unit).KitchenArea <= filter.KitchenArea.Hight);
            }
            if (filter.LivingArea != null)
            {
                realEstates = realEstates.Where(b => ((BedSpace)b.Unit).LivingArea >= filter.LivingArea.Low && ((BedSpace)b.Unit).LivingArea <= filter.LivingArea.Hight);
            }
            if (filter.TermOfRents?.Count > 0)
            {
                realEstates = realEstates.Where(b => filter.TermOfRents.All(t => ((BedSpace)b.Unit).TermOfRents.Contains(t)));
            }
            if (filter.TotalArea != null)
            {
                realEstates = realEstates.Where(b => ((BedSpace)b.Unit).TotalArea >= filter.TotalArea.Low && ((BedSpace)b.Unit).TotalArea <= filter.TotalArea.Hight);
            }
            if (filter.UnitProperties?.Count > 0)
            {
                realEstates = realEstates.Where(b => filter.UnitProperties.All(u => ((BedSpace)b.Unit).UnitProperties.Contains(u)));
            }

            return realEstates;
        }
    }
}
