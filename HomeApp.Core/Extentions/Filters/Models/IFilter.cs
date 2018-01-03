namespace HomeApp.Core.Extentions.Filters.Models
{
    /// <summary>
    /// Фильтр для запросов
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        /// Сколько записей взять
        /// </summary>
        int Take { get; set; }
        /// <summary>
        /// Сколько записей пропустить
        /// </summary>
        int Skip { get; set; }
    }
}
