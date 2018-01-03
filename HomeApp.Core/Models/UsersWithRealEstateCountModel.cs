using System.Collections.Generic;

namespace HomeApp.Core.Models
{
    /// <summary>
    /// Список пользователей с количеством объявлений
    /// </summary>
    public class UsersWithRealEstateCountModel
    {
        /// <summary>
        /// Пользователи с количеством объявлений
        /// </summary>
        public List<UserWithRealEstateCountModel> UsersWithRealEstateCount { get; set; }
        /// <summary>
        /// Всего пользователей с объявлениями
        /// </summary>
        public int Count { get; set; }
    }
}
