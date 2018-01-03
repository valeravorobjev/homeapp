using System.Collections.Generic;
using HomeApp.Core.Db.Entities.Models.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Core.Db.Entities.Models
{
    /// <summary>
    /// Койко место
    /// </summary>
    public class BedSpace: UnitBase
    {
        /// <summary>
        /// Этаж
        /// </summary>
        [BsonElement("floor")]
        [BsonIgnoreIfDefault]
        public byte Floor { get; set; }
        /// <summary>
        /// Количество пассажирских лифтов
        /// </summary>
        [BsonElement("passenger_lift_count")]
        [BsonIgnoreIfDefault]
        public int PassengerLiftCount { get; set; }
        /// <summary>
        /// Количество грузовых лифтов
        /// </summary>
        [BsonElement("freight_lift_count")]
        [BsonIgnoreIfDefault]
        public int FreightLiftCount { get; set; }
        /// <summary>
        /// Жилая площадь
        /// </summary>
        [BsonElement("living_area")]
        [BsonIgnoreIfDefault]
        public int LivingArea { get; set; }
        /// <summary>
        /// Площадь кухни
        /// </summary>
        [BsonElement("kitchen_area")]
        [BsonIgnoreIfDefault]
        public int KitchenArea { get; set; }
        /// <summary>
        /// Общая площадь
        /// </summary>
        [BsonElement("total_area")]
        [BsonIgnoreIfDefault]
        public int TotalArea { get; set; }
        /// <summary>
        /// Условия аренды
        /// </summary>
        [BsonElement("term_of_rents")]
        [BsonIgnoreIfNull]
        public List<TermOfRent> TermOfRents { get; set; }
        /// <summary>
        /// Свойства недвижимости
        /// </summary>
        [BsonElement("unit_properties")]
        [BsonIgnoreIfNull]
        public List<UnitProperty> UnitProperties { get; set; }
    }
}
