using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Core.Db.Entities.Models
{
    /// <summary>
    /// Адрес
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Страна
        /// </summary>
        [BsonElement("country")]
        [BsonIgnoreIfNull]
        public Element Country { get; set; }
        /// <summary>
        /// Регион (например, московская область)
        /// </summary>
        [BsonElement("region")]
        [BsonIgnoreIfNull]
        public Element Region { get; set; }
        /// <summary>
        /// Район (например, Каширскай район)
        /// </summary>
        [BsonElement("area")]
        [BsonIgnoreIfNull]
        public Element Area { get; set; }
        /// <summary>
        /// Город
        /// </summary>
        [BsonElement("sity")]
        [BsonIgnoreIfNull]
        public Element Sity { get; set; }
        /// <summary>
        /// Улица
        /// </summary>
        [BsonElement("street")]
        [BsonIgnoreIfNull]
        public Element Street { get; set; }
        /// <summary>
        /// Строение
        /// </summary>
        [BsonElement("street_number")]
        [BsonIgnoreIfNull]
        public Element StreetNumber { get; set; }
        /// <summary>
        /// Метно (название)
        /// </summary>
        [BsonElement("metro")]
        [BsonIgnoreIfNull]
        public Element Metro { get; set; }
        /// <summary>
        /// Время от метро в минутах
        /// </summary>
        [BsonElement("metro_time")]
        [BsonIgnoreIfDefault]
        public int MetroTime { get; set; }
        /// <summary>
        /// Гео данные
        /// </summary>
        [BsonElement("geo_data")]
        [BsonIgnoreIfNull]
        public GeoData GeoData { get; set; }
    }
}
