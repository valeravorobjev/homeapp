using System;
using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Core.Db.Entities.Models
{
    /// <summary>
    /// Персона. Конкретный пользователь, не компания. Например, обычный посетитель\клиент.
    /// </summary>
    public class Person: User
    {
        /// <summary>
        /// Имя
        /// </summary>
        [BsonElement("first_name")]
        [BsonIgnoreIfNull]
        public Element FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        [BsonElement("last_name")]
        [BsonIgnoreIfNull]
        public Element LastName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        [BsonElement("mid_name")]
        [BsonIgnoreIfNull]
        public Element MidName { get; set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        [BsonElement("date_birth")]
        [BsonIgnoreIfDefault]
        public DateTime? DateBirth { get; set; }
        /// <summary>
        /// Пол
        /// </summary>
        [BsonElement("sex")]
        [BsonIgnoreIfDefault]
        public bool? Sex { get; set; }
    }
}
