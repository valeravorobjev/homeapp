using HomeApp.Core.Db.Entities;
using HomeApp.Core.Extentions.Project.Models.Enums;
using MongoDB.Driver.Linq;

namespace HomeApp.Core.Extentions.Project
{
    /// <summary>
    /// Проекция для объектов недвижимости
    /// </summary>
    public static class RealEstateProjectExt
    {
        /// <summary>
        /// Проекция для коллекции объектов недвижимости
        /// </summary>
        /// <param name="realEstates">Объекты недвижимости</param>
        /// <param name="settings">Настройки</param>
        /// <returns></returns>
        public static IMongoQueryable<RealEstate> Project(this IMongoQueryable<RealEstate> realEstates, RealEstateProjectSettings settings)
        {
            if (settings == RealEstateProjectSettings.Short)
            {
                return realEstates.Select(r => new RealEstate
                {
                    Id = r.Id,
                    UserId = r.UserId,
                    PublishDate = r.PublishDate,
                    RealEstateStatus = r.RealEstateStatus,
                    Unit = r.Unit
                    //{
                    //    OperationType = r.Unit.OperationType,
                    //    ContractType = r.Unit.ContractType,
                    //    UnitType = r.Unit.UnitType,
                    //    UnitCategory = r.Unit.UnitCategory,
                    //    Seo = new Seo
                    //    {
                    //        DislikeCount = r.Unit.Seo.DislikeCount,
                    //        LikeCount = r.Unit.Seo.LikeCount,
                    //        Rating = r.Unit.Seo.Rating,
                    //        ShowCount = r.Unit.Seo.ShowCount
                    //    },
                    //    Address = r.Unit.Address,
                    //    PhotoMinPath400X266 = r.Unit.PhotoMinPath400X266,
                    //    PhotoMinPath500X500 = r.Unit.PhotoMinPath500X500,
                    //    CurrencyType = r.Unit.CurrencyType,
                    //    Cost = r.Unit.Cost
                    //}
                });
            }

            return realEstates;
        }
    }
}
