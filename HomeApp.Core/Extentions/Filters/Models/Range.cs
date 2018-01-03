namespace HomeApp.Core.Extentions.Filters.Models
{
    /// <summary>
    /// Диаппазон для разных типов данных
    /// </summary>
    public class Range<T>
    {
        /// <summary>
        /// Нижняя граница
        /// </summary>
        public T Low { get; set; }
        /// <summary>
        /// Верхняя граница
        /// </summary>
        public T Hight { get; set; }
    }
}
