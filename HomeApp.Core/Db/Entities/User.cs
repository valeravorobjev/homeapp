using System.Collections.Generic;
using HomeApp.Core.Db.Entities.Models;
using HomeApp.Core.Db.Entities.Models.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;

namespace HomeApp.Core.Db.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    [BsonIgnoreExtraElements]
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(Person), typeof(PersonProfessional), typeof(Realtor), typeof(Agency), typeof(Professional), typeof(Developer))]
    public class User
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [BsonId]
        public ObjectId Id { get; set; }
        /// <summary>
        /// Полиморфизм
        /// </summary>
        [BsonElement("_t")]
        [BsonIgnoreIfNull]
        public List<string> T { get; private set; }
        /// <summary>
        /// Тип пользователя
        /// </summary>
        [BsonElement("user_type")]
        public UserType UserType { get; set; }
        /// <summary>
        /// Определяет положение пользователя в системе (обычный пользователь, про, и т.д.)
        /// </summary>
        [BsonElement("user_mode")]
        public UserMode UserMode { get; set; }
        /// <summary>
        /// Идентификатор рекламной компании
        /// </summary>
        [BsonElement("ad_id")]
        public ObjectId AdId { get; set; }
        /// <summary>
        /// Телефон
        /// </summary>
        [BsonElement("phones")]
        [BsonIgnoreIfNull]
        public List<string> Phones { get; set; }
        /// <summary>
        /// Учетные данные
        /// </summary>
        [BsonElement("membership")]
        public Membership Membership { get; set; }
        /// <summary>
        /// Социальные сети
        /// </summary>
        [BsonElement("social_media")]
        [BsonIgnoreIfNull]
        public SocialMedia SocialMedia { get; set; }
        /// <summary>
        /// Маленькое фото
        /// </summary>
        [BsonElement("photo_min_path")]
        [BsonIgnoreIfNull]
        public string PhotoMinPath { get; set; }
        /// <summary>
        /// Основное фото
        /// </summary>
        [BsonElement("photo_path")]
        [BsonIgnoreIfNull]
        public string PhotoPath { get; set; }
        /// <summary>
        /// Основной язык
        /// </summary>
        [BsonElement("language")]
        [BsonIgnoreIfNull]
        public Language Language { get; set; }
        /// <summary>
        /// Поисковая оптимизация
        /// </summary>
        [BsonElement("seo")]
        [BsonIgnoreIfNull]
        public Seo Seo { get; set; }
        /// <summary>
        /// Комментарии пользователей
        /// </summary>
        [BsonElement("comments")]
        [BsonIgnoreIfNull]
        public List<Comment> Comments { get; set; }
        /// <summary>
        /// Адрес
        /// </summary>
        [BsonElement("address")]
        [BsonIgnoreIfNull]
        public Address Address { get; set; }
    }
}
