using HomeApp.Core.Db.Entities;

namespace HomeApp.Core.ViewModels
{
    /// <summary>
    /// Вью модель пользователя с количеством объявлений
    /// </summary>
    public class UserWithRealEstateCount
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
