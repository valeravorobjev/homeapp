namespace HomeApp.Core.Db.Entities.Models.Enums
{
    /// <summary>
    /// Тип дома
    /// </summary>
    public enum HouseType: byte
    {
        /// <summary>
        /// Тип не указан
        /// </summary>
        None = 0,
        /// <summary>
        /// Панельный
        /// </summary>
        Panel = 1,
        /// <summary>
        /// Монолитно кирпичный
        /// </summary>
        MonolithicBrick = 2,
        /// <summary>
        /// Монолитный
        /// </summary>
        Monolithic = 3,
        /// <summary>
        /// Блочный
        /// </summary>
        Block = 4,
        /// <summary>
        /// Кирпичный
        /// </summary>
        Brick = 5,
        /// <summary>
        /// Деревянный
        /// </summary>
        Wooden = 6,
        /// <summary>
        /// Сталинский
        /// </summary>
        Stalin = 7

    }
}
