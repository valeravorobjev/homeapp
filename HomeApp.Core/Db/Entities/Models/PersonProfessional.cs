﻿using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Core.Db.Entities.Models
{
    /// <summary>
    /// Характеристики профессионального пользователя (физ.лицо)
    /// </summary>
    public class PersonProfessional: Person
    {
        /// <summary>
        /// Опыт работы (с какого года работает)
        /// </summary>
        [BsonElement("from_year")]
        [BsonIgnoreIfNull]
        public int FromYear { get; set; }
        /// <summary>
        /// Регионы работы (название городов\регионов где работает профессионал)
        /// </summary>
        [BsonElement("work_regions")]
        [BsonIgnoreIfNull]
        public List<Element> WorkRegions { get; set; }
        /// <summary>
        /// Специализация
        /// </summary>
        [BsonElement("specialization")]
        [BsonIgnoreIfNull]
        public Specialization Specialization { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        [BsonElement("description")]
        [BsonIgnoreIfNull]
        public Element Description { get; set; }
        /// <summary>
        /// Сайт
        /// </summary>
        [BsonElement("site")]
        [BsonIgnoreIfNull]
        public string Site { get; set; }
        /// <summary>
        /// Образование
        /// </summary>
        [BsonElement("education")]
        [BsonIgnoreIfNull]
        public Element Education { get; set; }
    }
}
