namespace HomeApp.Core.Db
{
    /// <summary>
    /// Названия базы данных и коллекций
    /// </summary>
    public static class DbSet
    {
        /// <summary>
        /// База данных homeapp
        /// </summary>
        public const string DB_NAME = "homeapp";
        /// <summary>
        /// Фейковая база данных homeapp
        /// </summary>
        public const string DB_NAME_FAKE = "homeapp_fake";

        /// <summary>
        /// Пользователи
        /// </summary>
        public const string USERS_COLLECTION = "users";
        /// <summary>
        /// Сессии
        /// </summary>
        public const string SESSIONS_COLLECTION = "sessions";
        /// <summary>
        /// Объявления
        /// </summary>
        public const string REAL_ESTATES_COLLECTION = "realestates";
        /// <summary>
        /// Список рекламных компаний
        /// </summary>
        public const string ADS_COLLECTION = "ads";
    }
}
