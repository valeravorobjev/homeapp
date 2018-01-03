using System.Collections.Generic;
using HomeApp.Core.Db.Entities.Models.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Core.Db.Entities.Models
{
    /// <summary>
    /// Рекламная компания для объектов недвижимости
    /// </summary>
    public class RealEstateAd: Ad
    {
        /// <summary>
        /// Что включено
        /// </summary>
        [BsonElement("real_estate_ad_items")]
        public List<RealEstateAdItem> RealEstateAdItem { get; set; }
    }
}
