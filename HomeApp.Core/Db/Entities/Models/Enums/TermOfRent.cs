namespace HomeApp.Core.Db.Entities.Models.Enums
{
    /// <summary>
    /// Условия аренды
    /// </summary>
    public enum TermOfRent: byte
    {
        /// <summary>
        /// Тип не указан
        /// </summary>
        None = 0,
        /// <summary>
        /// Можно с детьми
        /// </summary>
        ChildrenAllowed = 1,
        /// <summary>
        /// Можно с животными
        /// </summary>
        PetsAllowed = 2

    }
}
