namespace HomeApp.Core.Extentions.Sorts.Models.Enums
{
    /// <summary>
    /// Сортировка для объектов недвижимости
    /// </summary>
    public enum RealEstateSort : byte
    {
        /// <summary>
        /// По популярности
        /// </summary>
        FeaturedListings = 0,
        /// <summary>
        /// По дате добавления (сначала новые)
        /// </summary>
        NewestToOldest = 1,
        /// <summary>
        /// По дате добавления (сначала старые)
        /// </summary>
        OldestToNewest = 2,
        /// <summary>
        /// По убыванию цены
        /// </summary>
        PriceHightToLow = 3,
        /// <summary>
        /// По возрастанию цены
        /// </summary>
        PriceLowToHigh = 4,
        /// <summary>
        /// По рейтингу
        /// </summary>
        Rating = 5
    }
}
