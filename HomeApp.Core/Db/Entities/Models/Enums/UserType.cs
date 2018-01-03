namespace HomeApp.Core.Db.Entities.Models.Enums
{
    /// <summary>
    /// Тип пользователя
    /// </summary>
    public enum UserType : byte
    {
        /// <summary>
        /// Тип не указан
        /// </summary>
        None = 0,
        /// <summary>
        /// Пользователь, человек, не компания
        /// </summary>
        Person = 1,
        /// <summary>
        /// Риэлтор
        /// </summary>
        Realtor = 2,
        /// <summary>
        /// Агенство
        /// </summary>
        Agency = 3,
        /// <summary>
        /// Застройщик
        /// </summary>
        Developer = 4
    }
}
