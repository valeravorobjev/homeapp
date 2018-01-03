namespace HomeApp.Core.Db.Entities.Models.Enums
{
    /// <summary>
    /// Услуга
    /// </summary>
    public enum AddService: byte
    {
        /// <summary>
        /// Тип не указан
        /// </summary>
        None = 0,
        /// <summary>
        /// Консультации
        /// </summary>
        Advice = 1,
        /// <summary>
        /// Строительство
        /// </summary>
        Development = 2,
        /// <summary>
        /// Кредитование
        /// </summary>
        Credit = 3,
        /// <summary>
        /// Ипотека
        /// </summary>
        Mortgage = 4,
        /// <summary>
        /// Поиск недвижимости
        /// </summary>
        SearchRealEstate = 5
    }
}
