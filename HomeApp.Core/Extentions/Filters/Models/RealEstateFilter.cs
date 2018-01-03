using System;
using System.Collections.Generic;
using HomeApp.Core.Db.Entities.Models.Enums;
using MongoDB.Bson;

namespace HomeApp.Core.Extentions.Filters.Models
{
    /// <summary>
    /// Фильтр для объявлений объектов недвижимости
    /// </summary>
    public class RealEstateFilter: IFilter
    {
        /// <summary>
        /// Сколько записей взять
        /// </summary>
        public int Take { get; set; }
        /// <summary>
        /// Сколько записей пропустить
        /// </summary>
        public int Skip { get; set; }
        /// <summary>
        /// Идентификатор пользователя, который продает или сдает данную недвижимость
        /// </summary>
        public ObjectId UserId { get; set; }
        /// <summary>
        /// Статус объявления
        /// </summary>
        public List<RealEstateStatus> RealEstateStatusList { get; set; }
        /// <summary>
        /// Дата публикации
        /// </summary>
        public Range<DateTime> PublishDate { get; set; }
    }
}
