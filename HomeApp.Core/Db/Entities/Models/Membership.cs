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
        /// <summary>
        /// Электронная почта
        /// </summary>
        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("date_reg")]
        public DateTime DateReg { get; set; }
    }
}
