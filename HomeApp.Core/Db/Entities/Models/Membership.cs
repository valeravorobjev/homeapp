using System;
using System.Collections.Generic;
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
        /// Пароль
        /// </summary>
        [BsonElement("password")]
        public string Password { get; set; }
        /// <summary>
        /// Электронная почта
        /// </summary>
        [BsonElement("email")]
        public string Email { get; set; }
        /// <summary>
        /// Соль
        /// </summary>
        [BsonElement("salt")]
        public int Salt { get; set; }
        /// <summary>
        /// Роли пользователя
        /// </summary>
        [BsonElement("user_roles")]
        public List<UserRole> UserRoles { get; set; }
        /// <summary>
        /// Статус пользователя
        /// </summary>
        [BsonElement("user_status")]
        public UserStatus UserStatus { get; set; }
        /// <summary>
        /// Дата регистарции
        /// </summary>
        [BsonElement("date_reg")]
        public DateTime DateReg { get; set; }
        /// <summary>
        /// Код активации
        /// </summary>
        [BsonElement("active_code")]
        public Guid ActiveCode { get; set; }
    }
}
