namespace HomeApp.Core.Db.Entities.Models.Enums
{
    /// <summary>
    /// Типы недвижимости
    /// </summary>
    public enum UnitCategory: byte
    {
        /// <summary>
        /// Тип не указан
        /// </summary>
        None = 0,
        /// <summary>
        /// Жилая недвижимость
        /// </summary>
        Residential = 1,
        /// <summary>
        /// Загородная недвижимость
        /// </summary>
        Country = 2,
        /// <summary>
        /// Комерческая недвижимость
        /// </summary>
        Commercial = 3
    }
}
