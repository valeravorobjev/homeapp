using System.Collections.Generic;
using HomeApp.Core.Db.Entities.Models;
using HomeApp.Core.Db.Entities.Models.Enums;

namespace HomeApp.Core.Extentions.Filters.Models
{
    /// <summary>
    /// Фильтр пользователя
    /// </summary>
    public class UserFilter
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
        /// Адрес
        /// </summary>
        public Address Address { get; set; }
        /// <summary>
        /// Определяет положение пользователя в системе (обычный пользователь, про, и т.д.)
        /// </summary>
        public List<UserMode> UserModes { get; set; }
        /// <summary>
        /// Тип пользователя
        /// </summary>
        public List<UserType> UserTypes { get; set; }
    }
}
