using System.Collections.Generic;
using HomeApp.Core.Db.Entities.Models.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Core.Db.Entities.Models
{
    /// <summary>
    /// Специализация
    /// </summary>
    [BsonIgnoreExtraElements]
    public class Specialization
    {
        /// <summary>
        /// Аренда недвижимости (типы недвижимости, которыми занимается риэлтор)
        /// </summary>
        [BsonElement("rental_properties")]
        [BsonIgnoreIfNull]
        public List<UnitCategory> RentalProperties { get; set; }
        /// <summary>
        /// Продажа недвижимости
        /// </summary>
        [BsonElement("estate_sales")]
        [BsonIgnoreIfNull]
        public List<UnitCategory> EstateSales { get; set; }
        /// <summary>
        /// Услуги
        /// </summary>
        [BsonElement("add_services")]
        [BsonIgnoreIfNull]
        public List<AddService> AddServices { get; set; }
    }
}