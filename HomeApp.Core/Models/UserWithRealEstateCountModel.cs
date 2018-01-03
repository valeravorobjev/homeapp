using HomeApp.Core.Db.Entities;

namespace HomeApp.Core.Models
{
    /// <summary>
    /// Вью модель пользователя с количеством объявлений
    /// </summary>
    public class UserWithRealEstateCountModel
    {
        /// <summary>
        /// Пользователь
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Количество объявлений
        /// </summary>
        public int RealEstateCount { get; set; }
    }
}
