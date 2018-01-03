namespace HomeApp.Core.Db.Entities.Models.Enums
{
    /// <summary>
    /// Типы операций с недвижимостью
    /// </summary>
    public enum OperationType: byte
    {
        /// <summary>
        /// Тип не указан
        /// </summary>
        None = 0,
        /// <summary>
        /// Продажа
        /// </summary>
        Sale = 1,
        /// <summary>
        /// Сдать в аренду\арендавать
        /// </summary>
        Rent = 2,
        /// <summary>
        /// Длительная аренда\сдача в аренду
        /// </summary>
        LongTermRental = 3,
        /// <summary>
        /// Посуточная аренда
        /// </summary>
        DailyRent = 4
    }
}
