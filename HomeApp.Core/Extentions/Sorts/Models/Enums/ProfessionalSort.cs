namespace HomeApp.Core.Extentions.Sorts.Models.Enums
{
    /// <summary>
    /// Тип сортировки для профессионалов
    /// </summary>
    public enum ProfessionalSort: byte
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
        /// По опыту работы (с какого года работает)
        /// </summary>
        FromYear = 3,
        /// <summary>
        /// По дате регистрации\добавления в homeapp.pro
        /// </summary>
        DateReg = 4
    }
}
