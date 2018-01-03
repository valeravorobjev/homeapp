namespace HomeApp.Core.Db.Entities.Models.Enums
{
    /// <summary>
    /// Состояние рекламной компании
    /// </summary>
    public enum AdStatus: byte
    {
        /// <summary>
        /// Тип не указан
        /// </summary>
        None = 0,
        /// <summary>
        /// Активна
        /// </summary>
        Active = 1,
        /// <summary>
        /// Не активна (истек срок)
        /// </summary>
        Inactive = 2
    }
}
