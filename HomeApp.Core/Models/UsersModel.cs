using System.Collections.Generic;
using HomeApp.Core.Db.Entities;

namespace HomeApp.Core.Models
{
    /// <summary>
    /// Модель для представления. Список пользователей
    /// </summary>
    public class UsersModel
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
