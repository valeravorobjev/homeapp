using HomeApp.Core.Db.Entities.Models;

namespace HomeApp.Core.Extentions.Filters.Models
{
    /// <summary>
    /// Фильтр для риэлторов
    /// </summary>
    public class RialtorFilter: PersonProfessionalFilter
    {
        /// <summary>
        /// Название специальности
        /// </summary>
        public Element Job { get; set; }
    }
}
