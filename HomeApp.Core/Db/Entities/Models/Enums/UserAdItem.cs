
namespace HomeApp.Core.Db.Entities.Models.Enums
{
    /// <summary>
    /// Элемент рекламной компании для пользователя
    /// </summary>
    public enum UserAdItem: byte
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
        /// Топ 50
        /// </summary>
        Top50 = 2,
        /// <summary>
        /// Топ 12
        /// </summary>
        Top12 = 3,
        /// <summary>
        /// Топ 9
        /// </summary>
        Top9 = 4
    }
}
