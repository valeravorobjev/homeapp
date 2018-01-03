using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Core.Db.Entities.Models
{
    /// <summary>
    /// Данные по рекламе
    /// </summary>
    public class Advertising
    {
        /// <summary>
        /// Идентификатор рекламы
        /// </summary>
        [BsonElement("ad_id")]
        public ObjectId AdId { get; set; }
        /// <summary>
        /// Дата начала
        /// </summary>
        [BsonElement("date_start")]
        public DateTime DateStart { get; set; }
        /// <summary>
        /// Дата окончания
        /// </summary>
        [BsonElement("date_end")]
        public DateTime DateEnd { get; set; }

    }
}
