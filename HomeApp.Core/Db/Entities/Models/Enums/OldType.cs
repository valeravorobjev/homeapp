namespace HomeApp.Core.Db.Entities.Models.Enums
{
    /// <summary>
    /// Тип возраста недвижимости
    /// </summary>
    public enum OldType: byte
    {
        /// <summary>
        /// Тип не указан
        /// </summary>
        None = 0,
        /// <summary>
        /// Новостнойка
        /// </summary>
        New = 1,
        /// <summary>
        /// Вторичное жильё
        /// </summary>
        Resale = 2
    }
}
