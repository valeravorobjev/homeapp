using HomeApp.Core.Db.Entities;
using HomeApp.Core.Extentions.Sorts.Models.Enums;
using MongoDB.Driver.Linq;

namespace HomeApp.Core.Extentions.Sorts
{
    /// <summary>
    /// Методы расширения для сортировки объектов недвижимости
    /// </summary>
    public static class RealEstateSortExt
    {
        /// <summary>
        /// Сортирует объекты недвижимости
        /// </summary>
        /// <param name="realEstates">Объекты недвижимости</param>
        /// <param name="sortType">Тип сортировки</param>
        /// <returns></returns>
        public static IMongoQueryable<RealEstate> Sort(this IMongoQueryable<RealEstate> realEstates, RealEstateSort sortType)
        {
            switch (sortType)
            {
                case RealEstateSort.FeaturedListings:
                    {
                        realEstates = realEstates.OrderByDescending(r => r.Unit.Seo.ShowCount);
                        break;
                    }
                case RealEstateSort.NewestToOldest:
                    {
                        realEstates = realEstates.OrderByDescending(r => r.PublishDate);
                        break;
                    }
                case RealEstateSort.OldestToNewest:
                    {
                        realEstates = realEstates.OrderBy(r => r.PublishDate);
                        break;
                    }
                case RealEstateSort.PriceHightToLow:
                    {
                        realEstates = realEstates.OrderByDescending(r => r.Unit.Cost);
                        break;
                    }
                case RealEstateSort.PriceLowToHigh:
                    {
                        realEstates = realEstates.OrderBy(r => r.Unit.Cost);
                        break;
                    }
                    case RealEstateSort.Rating:
                {
                    realEstates = realEstates.OrderByDescending(r => r.Unit.Seo.Rating);
                    break;
                }
                default:
                    {
                        break;
                    }
            }

            return realEstates;
        }
    }
}
