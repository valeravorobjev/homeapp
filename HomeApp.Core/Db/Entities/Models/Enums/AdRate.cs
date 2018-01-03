namespace HomeApp.Core.Db.Entities.Models.Enums
{
    /// <summary>
    /// Тариф (позиция) определяет сегмент для пользователя. Чем выше позиция, тем больше у пользователя привелегий.
    /// </summary>
    public enum AdRate: byte
    {
        /// <summary>
        /// Тип не указан
        /// </summary>
        None = 0,
        /// <summary>
        /// Премиум пользователь
        /// </summary>
        Premium = 1,
        /// <summary>
        /// Золотой пользователь
        /// </summary>
        Gold = 2,
        /// <summary>
        /// Платиновый
        /// </summary>
        Platium = 3
    }
}
