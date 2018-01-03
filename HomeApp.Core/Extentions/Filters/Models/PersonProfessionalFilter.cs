using System.Collections.Generic;
using HomeApp.Core.Db.Entities.Models;

namespace HomeApp.Core.Extentions.Filters.Models
{
    /// <summary>
    /// Персона профессионал фильтр
    /// </summary>
    public class PersonProfessionalFilter: PersonFilter
    {
        /// <summary>
        /// Опыт работы (с какого года работает)
        /// </summary>
        public Range<int> FromYear { get; set; }
        /// <summary>
        /// Регионы работы (название городов\регионов где работает профессионал)
        /// </summary>
        public List<string> WorkRegions { get; set; }
        /// <summary>
        /// Специализация
        /// </summary>
        public Specialization Specialization { get; set; }
        /// <summary>
        /// Сайт
        /// </summary>
        public string Site { get; set; }
    }
}
