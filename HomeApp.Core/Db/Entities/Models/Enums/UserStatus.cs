namespace HomeApp.Core.Db.Entities.Models.Enums
{
    /// <summary>
    /// Статус пользователя
    /// </summary>
    public enum UserStatus : byte
    {
        /// <summary>
        /// Тип не указан
        /// </summary>
        None = 0,
        /// <summary>
        /// Активен
        /// </summary>
        Active = 1,
        /// <summary>
        /// Забанен
        /// </summary>
        Banned = 2,
        /// <summary>
        /// Ожидает подтверждения (выслан код активации на email , после регистрации)
        /// </summary>
        OnConfirmation = 3
    }
}
