namespace HomeApp.Core.Db.Entities.Models.Enums
{
    /// <summary>
    /// Мод пользователя
    /// </summary>
    public enum UserMode: byte
    {
        /// <summary>
        /// Тип не указан
        /// </summary>
        None = 0,
        /// <summary>
        /// Обычный пользователь
        /// </summary>
        Normal = 1,
        /// <summary>
        /// Про пользователь
        /// </summary>
        Pro = 2
    }
}
