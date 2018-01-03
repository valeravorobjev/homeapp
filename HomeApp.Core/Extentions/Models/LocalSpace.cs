namespace HomeApp.Core.Extentions.Models
{
    /// <summary>
    /// Модель локализации
    /// </summary>
    public class LocalSpace
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public LocalSpace() { }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="single">Единственное число</param>
        public LocalSpace(string single)
        {
            Single = single;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="single">Единственное число</param>
        /// <param name="plural">Множественное</param>
        public LocalSpace(string single, string plural)
        {
            Single = single;
            Plural = plural;
        }

        /// <summary>
        /// Единственное число
        /// </summary>
        public string Single { get; set; }
        /// <summary>
        /// Множественное
        /// </summary>
        public string Plural { get; set; }
    }
}
