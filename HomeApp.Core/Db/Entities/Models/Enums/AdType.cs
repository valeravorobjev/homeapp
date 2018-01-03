namespace HomeApp.Core.Db.Entities.Models.Enums
{
    /// <summary>
    /// Тип рекламной компании
    /// </summary>
    public enum AdType: byte
    {
        /// <summary>
        /// Тип не указан
        /// </summary>
        None = 0,
        /// <summary>
        /// Рекламная компания для объектов недвижимости
        /// </summary>
        RealEstate = 1,
        /// <summary>
        /// Рекламная компания для пользователя
        /// </summary>
        User = 2
    }
}
