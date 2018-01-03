namespace HomeApp.Core.Db.Entities.Models.Enums
{
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public enum UserRole : byte
    {
        /// <summary>
        /// Тип не указан
        /// </summary>
        None = 0,
        /// <summary>
        /// Обычный пользователь
        /// </summary>
        User = 1,
        /// <summary>
        /// Администратор
        /// </summary>
        Administrator = 2,
        /// <summary>
        /// Модератор
        /// </summary>
        Moderator = 3
    }
}
