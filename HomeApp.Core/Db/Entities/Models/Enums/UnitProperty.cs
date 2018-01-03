namespace HomeApp.Core.Db.Entities.Models.Enums
{
    /// <summary>
    /// Свойства недвижимости
    /// </summary>
    public enum UnitProperty : byte
    {
        /// <summary>
        /// Тип не указан
        /// </summary>
        None = 0,
        /// <summary>
        /// Сдаётся впервые
        /// </summary>
        FirstTime = 1,
        /// <summary>
        /// Кондиционер
        /// </summary>
        Conditioner = 2,
        /// <summary>
        /// Машиноместо
        /// </summary>
        Parking = 3,
        /// <summary>
        /// Лоджия\Балкон
        /// </summary>
        Loggia = 4,
        /// <summary>
        /// Без мебели
        /// </summary>
        Unfurnished = 5,
        /// <summary>
        /// Свежий ремонт
        /// </summary>
        FreshRepair = 6,
        /// <summary>
        /// Раздельный санузел
        /// </summary>
        SeparateBathroom = 7,
        /// <summary>
        /// Студийная планировка
        /// </summary>
        StudioLayout = 8,
        /// <summary>
        /// Лифт
        /// </summary>
        Lift = 9,
        /// <summary>
        /// Огороженная территория
        /// </summary>
        FencedArea = 10,
        /// <summary>
        /// Кухонная мебель
        /// </summary>
        KitchenFurnitur = 11,
        /// <summary>
        /// Стиральная машина
        /// </summary>
        WashingMachine = 12,
        /// <summary>
        /// Посудомоечная машина
        /// </summary>
        Dishwasher = 13,
        /// <summary>
        /// Холодильник
        /// </summary>
        Fridge = 14,
        /// <summary>
        /// Телефон
        /// </summary>
        Phone = 15,
        /// <summary>
        /// Интернет
        /// </summary>
        Internet = 16,
        /// <summary>
        /// Телевизор
        /// </summary>
        Tv = 17

    }
}
