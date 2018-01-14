using System;
using HomeApp.Core.Db.Entities.Models.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Core.Db.Entities.Models
{
    /// <summary>
    /// Учетная запись
    /// </summary>
    public class Membership
    {
        /// <summary>
        /// Логин
        /// </summary>
        [BsonElement("login")]
        public string Login { get; set; }
        /// <summary>
        /// Электронная почта
        /// </summary>
        [BsonElement("email")]
        public string Email { get; set; }
        /// <summary>
        /// Дата регистрации
        /// </summary>
        [BsonElement("date_reg")]
        public DateTime DateReg { get; set; }
        /// <summary>
        /// Статус пользователя
        /// </summary>
        [BsonElement("user_status")]
        public UserStatus UserStatus { get; set; }
    }
}
