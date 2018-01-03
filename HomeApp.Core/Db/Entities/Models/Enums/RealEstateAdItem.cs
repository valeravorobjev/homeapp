namespace HomeApp.Core.Db.Entities.Models.Enums
{
    /// <summary>
    /// Привелегии, которые дает рекламная компания
    /// </summary>
    public enum RealEstateAdItem: byte
    {
        /// <summary>
        /// Тип не указан
        /// </summary>
        None = 0,
        /// <summary>
        /// Топ 100
        /// </summary>
        Top100 = 1,
        /// <summary>
        /// Топ 10
        /// </summary>
        Top10 = 2,
        /// <summary>
        /// Топ 4
        /// </summary>
        Top4 = 3,
        /// <summary>
        /// Лента в хэдере сайта на главной странице
        /// </summary>
        AdHeadLine = 4
    }
}
