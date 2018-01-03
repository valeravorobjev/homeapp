using System.Collections.Generic;
using HomeApp.Core.Db.Entities.Models.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Core.Db.Entities.Models
{
    /// <summary>
    /// Комната
    /// </summary>
    public class Room: BedSpace
    {
        /// <summary>
        /// Количество комнат
        /// </summary>
        [BsonElement("room_count")]
        [BsonIgnoreIfDefault]
        public byte RoomCount { get; set; }
        /// <summary>
        /// Тип дома
        /// </summary>
        [BsonElement("house_type")]
        [BsonIgnoreIfDefault]
        public HouseType HouseType { get; set; }
        /// <summary>
        /// Залог
        /// </summary>
        [BsonElement("pledge")]
        [BsonIgnoreIfDefault]
        public float Pledge { get; set; }
        /// <summary>
        /// Предоплата срок (месяцы)
        /// </summary>
        [BsonElement("prepayment_term")]
        [BsonIgnoreIfDefault]
        public byte PrepaymentTerm { get; set; }
        /// <summary>
        /// Оплата комунальных услуг
        /// </summary>
        [BsonElement("utility_payment")]
        [BsonIgnoreIfDefault]
        public UtilityPayment UtilityPayment { get; set; }
        /// <summary>
        /// Площадь комнат
        /// </summary>
        [BsonElement("room_areas")]
        [BsonIgnoreIfNull]
        public List<int> RoomAreas { get; set; }
        /// <summary>
        /// Высота потолков (метры)
        /// </summary>
        [BsonElement("ceiling_height")]
        [BsonIgnoreIfDefault]
        public float CeilingHeight { get; set; }
        /// <summary>
        /// Количество сан узлов
        /// </summary>
        [BsonElement("bathroom_count")]
        [BsonIgnoreIfDefault]
        public byte BathroomCount { get; set; }
        /// <summary>
        /// Количество совмещенных санузлов
        /// </summary>
        [BsonElement("combined_bathroom_count")]
        [BsonIgnoreIfDefault]
        public byte CombinedBathroomCount { get; set; }
    }
}
