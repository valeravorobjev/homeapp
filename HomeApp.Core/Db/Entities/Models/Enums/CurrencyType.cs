namespace HomeApp.Core.Db.Entities.Models.Enums
{
    /// <summary>
    /// Тип валюты
    /// </summary>
    public enum CurrencyType : byte
    {
        /// <summary>
        /// Тип не указан
        /// </summary>
        None = 0,
        /// <summary>
        /// Рубли
        /// </summary>
        RUR = 1,
        /// <summary>
        /// Доллары
        /// </summary>
        USD = 2,
        /// <summary>
        /// Евро
        /// </summary>
        EUR = 3
    }
}
