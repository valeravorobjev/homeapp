using System;
using HomeApp.Core.Db.Entities.Models;

namespace HomeApp.Core.Extentions.Filters.Models
{
    /// <summary>
    /// Фильтр по персоне
    /// </summary>
    public class PersonFilter: UserFilter
    {
        /// <summary>
        /// Имя
        /// </summary>
        public Element FirstName { get; set; }
        /// <summary>
        /// Фамилия
        /// </summary>
        public Element LastName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public Element MidName { get; set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime? DateBirth { get; set; }
        /// <summary>
        /// Пол
        /// </summary>
        public bool? Sex { get; set; }
    }
}
