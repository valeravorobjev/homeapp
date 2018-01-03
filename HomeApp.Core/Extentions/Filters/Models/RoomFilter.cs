using System.Collections.Generic;
using HomeApp.Core.Db.Entities.Models.Enums;

namespace HomeApp.Core.Extentions.Filters.Models
{
    /// <summary>
    /// Фильтр для комнаты
    /// </summary>
    public class RoomFilter: BedSpaceFilter
    {
        /// <summary>
        /// Количество комнат
        /// </summary>
        public List<byte> RoomCounts { get; set; }
        /// <summary>
        /// Типы дома
        /// </summary>
        public List<HouseType> HouseTypes { get; set; }
        /// <summary>
        /// Залог
        /// </summary>
        public Range<float> Pledge { get; set; }
        /// <summary>
        /// Предоплата срок (месяцы)
        /// </summary>
        public byte? PrepaymentTerm { get; set; }
        /// <summary>
        /// Оплата комунальных услуг
        /// </summary>
        public UtilityPayment? UtilityPayment { get; set; }
        /// <summary>
        /// Количество сан узлов
        /// </summary>
        public List<byte> BathroomCounts { get; set; }
        /// <summary>
        /// Количество совмещенных санузлов
        /// </summary>
        public List<byte> CombinedBathroomCounts { get; set; }
    }
}
