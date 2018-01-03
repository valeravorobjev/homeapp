using System.Collections.Generic;
using HomeApp.Core.Db.Entities.Models.Enums;

namespace HomeApp.Core.Extentions.Filters.Models
{
    /// <summary>
    /// Фильтр для квартиры
    /// </summary>
    public class FlatFilter: RoomFilter
    {
        /// <summary>
        /// Типы возраста недвижимости
        /// </summary>
        public List<OldType> OldTypes { get; set; }
    }
}
