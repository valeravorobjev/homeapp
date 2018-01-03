using System.Collections.Generic;
using HomeApp.Core.Db.Entities;

namespace HomeApp.Core.ViewModels
{
    /// <summary>
    /// Модель для представления. Список пользователей
    /// </summary>
    public class UserList
    {
        /// <summary>
        /// Список пользователей
        /// </summary>
        public List<User> Users { get; set; }
        /// <summary>
        /// Количество пользователей в базе
        /// </summary>
        public int Count { get; set; }
    }
}
