namespace HomeApp.Core.Db.Entities.Models.Enums
{
    /// <summary>
    /// Статус объявления
    /// </summary>
    public enum RealEstateStatus :byte
    {
        /// <summary>
        /// Тип не указан
        /// </summary>
        None = 0,
        /// <summary>
        /// Объявление активно
        /// </summary>
        Active = 1,
        /// <summary>
        /// Истек срок публикации объявления
        /// </summary>
        Expired = 2,
        /// <summary>
        /// Продано
        /// </summary>
        Sold = 3,
        /// <summary>
        /// Сдано в аренду
        /// </summary>
        Rented = 4,
        /// <summary>
        /// Объявление снято с публикации пользователем
        /// </summary>
        RemovedByUser = 5

    }
}
