namespace HomeApp.Core.Extentions.Sorts.Models.Enums
{
    /// <summary>
    /// Тип сортировки для персон
    /// </summary>
    public enum PersonSort: byte
    {
        /// <summary>
        /// По популярности
        /// </summary>
        FeaturedListings = 0,
        /// <summary>
        /// По рейтингу
        /// </summary>
        Rating = 1,
        /// <summary>
        /// По алфавиту
        /// </summary>
        Alphabet = 2,
        /// <summary>
        /// По дате регистрации\добавления в homeapp.pro
        /// </summary>
        DateReg = 4
    }
}
