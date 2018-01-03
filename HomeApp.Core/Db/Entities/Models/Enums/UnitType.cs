namespace HomeApp.Core.Db.Entities.Models.Enums
{
    /// <summary>
    /// Тип объекта недвижимости
    /// </summary>
    public enum UnitType: byte
    {
        /// <summary>
        /// Тип не указан
        /// </summary>
        None = 0,
        /// <summary>
        /// Квартира
        /// </summary>
        Flat = 1,
        /// <summary>
        /// Комната
        /// </summary>
        Room = 2,
        /// <summary>
        /// Койко место
        /// </summary>
        BedSpace = 3,
        /// <summary>
        /// Дом
        /// </summary>
        House = 4,
        /// <summary>
        /// Часть дома
        /// </summary>
        PartOfHouse = 5,
        /// <summary>
        /// Таунхауз
        /// </summary>
        Townhouse = 6,
        /// <summary>
        /// Офис
        /// </summary>
        Office = 7,
        /// <summary>
        /// Торговая площадь
        /// </summary>
        RetailSpace = 8,
        /// <summary>
        /// Склад
        /// </summary>
        Warehouse = 9,
        /// <summary>
        /// Пансионат
        /// </summary>
        Guesthouse = 10,
        /// <summary>
        /// Отель
        /// </summary>
        Hotel = 11,
        /// <summary>
        /// Общепит
        /// </summary>
        Catering = 12,
        /// <summary>
        /// Гараж
        /// </summary>
        Garage = 13,
        /// <summary>
        /// Производство
        /// </summary>
        Industry = 14,
        /// <summary>
        /// Автосервис
        /// </summary>
        CarService = 15,
        /// <summary>
        /// Готовый бизнес
        /// </summary>
        ReadyBusiness = 16,
        /// <summary>
        /// Здание
        /// </summary>
        Building = 17,
        /// <summary>
        /// Бытовые услуги
        /// </summary>
        HouseholdServices = 18
    }
}
