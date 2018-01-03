using System.Collections.Generic;
using HomeApp.Core.Db.Entities.Models.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Core.Db.Entities.Models
{
    /// <summary>
    /// Объект недвижимости
    /// </summary>
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(BedSpace),typeof(Room),typeof(Flat), typeof(House))]
    public class UnitBase
    {
        /// <summary>
        /// Типы операций с недвижимостью
        /// </summary>
        [BsonElement("operation_type")]
        public OperationType OperationType { get; set; }
        /// <summary>
        /// Тип договора
        /// </summary>
        [BsonElement("contract_type")]
        [BsonIgnoreIfDefault]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// Типы недвижимости
        /// </summary>
        [BsonElement("unit_type")]
        public UnitType UnitType { get; set; }
        /// <summary>
        /// Категория юнита
        /// </summary>
        [BsonElement("unit_category")]
        public UnitCategory UnitCategory { get; set; }
        /// <summary>
        /// Рекламная информация
        /// </summary>
        [BsonElement("advertising")]
        [BsonIgnoreIfNull]
        public Advertising Advertising { get; set; }
        /// <summary>
        /// Содержит данные для поисковой оптимизации
        /// </summary>
        [BsonElement("seo")]
        public Seo Seo { get; set; }
        /// <summary>
        /// Адрес
        /// </summary>
        [BsonElement("address")]
        public Address Address { get; set; }
        /// <summary>
        /// Фотографии
        /// </summary>
        [BsonElement("photo_paths")]
        [BsonIgnoreIfNull]
        public List<string> PhotoPaths { get; set; }
        /// <summary>
        /// Минимизированная фотограффия размером w400 на h266
        /// </summary>
        [BsonElement("photo_min_path_400x266")]
        public string PhotoMinPath400X266 { get; set; }
        /// <summary>
        /// Минимизированная фотограффия размером w500 на h500
        /// </summary>
        [BsonElement("photo_min_path_500x500")]
        public string PhotoMinPath500X500 { get; set; }
        /// <summary>
        /// Тип валюты
        /// </summary>
        [BsonElement("currency_type")]
        public CurrencyType CurrencyType { get; set; }
        /// <summary>
        /// Стоимость
        /// </summary>
        [BsonElement("cost")]
        public float Cost { get; set; }
        /// <summary>
        /// Коммисия процент
        /// </summary>
        [BsonElement("commission_perc")]
        [BsonIgnoreIfDefault]
        public float CommissionPerc { get; set; }
        /// <summary>
        /// Коммисия фиксированная цена
        /// </summary>
        [BsonElement("commission_fix")]
        [BsonIgnoreIfDefault]
        public float CommissionFix { get; set; }
        /// <summary>
        /// Возможна эпотека
        /// </summary>
        [BsonElement("is_mortgage")]
        [BsonIgnoreIfDefault]
        public bool IsMortgage { get; set; }
        /// <summary>
        /// Подробное описание
        /// </summary>
        [BsonElement("description")]
        [BsonIgnoreIfNull]
        public Element Description { get; set; }
        /// <summary>
        /// Год постройки дома
        /// </summary>
        [BsonElement]
        [BsonIgnoreIfNull]
        public int YearBuilt { get; set; }
    }
}
