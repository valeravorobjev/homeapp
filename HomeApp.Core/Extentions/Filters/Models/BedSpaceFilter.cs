using System.Collections.Generic;
using HomeApp.Core.Db.Entities.Models.Enums;

namespace HomeApp.Core.Extentions.Filters.Models
{
    /// <summary>
    /// Фильтр для койко места
    /// </summary>
    public class BedSpaceFilter: UnitBaseFilter
    {
        /// <summary>
        /// Этажы
        /// </summary>
        public List<byte> Floors { get; set; }
        /// <summary>
        /// Жилая площадь
        /// </summary>
        public Range<int> LivingArea { get; set; }
        /// <summary>
        /// Площадь кухни. Нижняя граница
        /// </summary>
        public Range<int> KitchenArea { get; set; }
        /// <summary>
        /// Общая площадь. Нижняя граница
        /// </summary>
        public Range<int> TotalArea { get; set; }
        /// <summary>
        /// Условия аренды
        /// </summary>
        public List<TermOfRent> TermOfRents { get; set; }
        /// <summary>
        /// Свойства объекта недвижимости
        /// </summary>
        public List<UnitProperty> UnitProperties { get; set; }
    }
}
