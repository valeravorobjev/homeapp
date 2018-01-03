namespace HomeApp.Core.Db.Entities.Models.Enums
{
    /// <summary>
    /// Оплата комунальных услуг
    /// </summary>
    public enum UtilityPayment: byte
    {   
        /// <summary>
        /// Тип не указан
        /// </summary>
        None = 0,
        /// <summary>
        /// Все включено
        /// </summary>
        AllInclusive = 1,
        /// <summary>
        /// Все включено без счетчиков
        /// </summary>
        AllInclusiveWithoutMeters = 2,
        /// <summary>
        /// Комунальные услуги не включены в стоимость (все оплачивает арендатор)
        /// </summary>
        NotIncluded = 3
    }
}
