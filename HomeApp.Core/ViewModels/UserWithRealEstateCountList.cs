using System.Collections.Generic;

namespace HomeApp.Core.ViewModels
{
    /// <summary>
    /// Список пользователей с количеством объявлений
    /// </summary>
    public class UserWithRealEstateCountList
    {
        /// <summary>
        /// Пользователи с количеством объявлений
        /// </summary>
        public List<UserWithRealEstateCount> UsersWithRealEstateCount { get; set; }
        /// <summary>
        /// Всего пользователей с объявлениями
        /// </summary>
        public int Count { get; set; }
    }
}
