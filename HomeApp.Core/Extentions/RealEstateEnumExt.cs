using System.Collections.Generic;
using System.Linq;
using HomeApp.Core.Db.Entities.Models.Enums;
using HomeApp.Core.Extentions.Models;

namespace HomeApp.Core.Extentions
{
    /// <summary>
    /// Расширения для enums класса RealEstate
    /// </summary>
    public static class RealEstateEnumExt
    {
        /// <summary>
        /// Переврдит перечисление в строку для соответствующиго языка
        /// </summary>
        /// <param name="operationType">Тип операции с недвижимостью</param>
        /// <param name="language">Язык</param>
        /// <returns></returns>
        public static LocalSpace ToLocalString(this OperationType operationType, Language language)
        {
            List<KeyValuePair<OperationType, LocalSpace>> paramsRu = new List<KeyValuePair<OperationType, LocalSpace>>();
            paramsRu.Add(new KeyValuePair<OperationType, LocalSpace>(OperationType.DailyRent, new LocalSpace("Посуточная аренда")));
            paramsRu.Add(new KeyValuePair<OperationType, LocalSpace>(OperationType.LongTermRental, new LocalSpace("Длительная аренда")));
            paramsRu.Add(new KeyValuePair<OperationType, LocalSpace>(OperationType.Rent, new LocalSpace("Аренда")));
            paramsRu.Add(new KeyValuePair<OperationType, LocalSpace>(OperationType.Sale, new LocalSpace("Продажа")));


            if (language == Language.Ru)
                return paramsRu.First(kv => kv.Key == operationType).Value;
            return new LocalSpace();
        }

        /// <summary>
        /// Переврдит перечисление в строку для соответствующиго языка
        /// </summary>
        /// <param name="realEstateType">Тип недвижимости</param>
        /// <param name="language">Тип языка</param>
        /// <returns></returns>
        public static LocalSpace ToLocalString(this UnitCategory realEstateType, Language language)
        {
            List<KeyValuePair<UnitCategory, LocalSpace>> paramsRu = new List<KeyValuePair<UnitCategory, LocalSpace>>();
            paramsRu.Add(new KeyValuePair<UnitCategory, LocalSpace>(UnitCategory.Commercial, new LocalSpace("Комерческая недвижимость")));
            paramsRu.Add(new KeyValuePair<UnitCategory, LocalSpace>(UnitCategory.Country, new LocalSpace("Загородная недвижимость")));
            paramsRu.Add(new KeyValuePair<UnitCategory, LocalSpace>(UnitCategory.Residential, new LocalSpace("Жилая недвижимость")));

            if (language == Language.Ru)
                return paramsRu.First(kv => kv.Key == realEstateType).Value;

            return new LocalSpace();
        }

        /// <summary>
        /// Переврдит перечисление в строку для соответствующиго языка
        /// </summary>
        /// <param name="unitType">Тип оьъекта недвижимости</param>
        /// <param name="language">Язык</param>
        /// <returns></returns>
        public static LocalSpace ToLocalString(this UnitType unitType, Language language)
        {
            List<KeyValuePair<UnitType, LocalSpace>> paramsRu = new List<KeyValuePair<UnitType, LocalSpace>>();
            paramsRu.Add(new KeyValuePair<UnitType, LocalSpace>(UnitType.BedSpace, new LocalSpace("Койко место")));
            paramsRu.Add(new KeyValuePair<UnitType, LocalSpace>(UnitType.Building, new LocalSpace("Здание")));
            paramsRu.Add(new KeyValuePair<UnitType, LocalSpace>(UnitType.CarService, new LocalSpace("Автосервис")));
            paramsRu.Add(new KeyValuePair<UnitType, LocalSpace>(UnitType.Catering, new LocalSpace("Общепит")));
            paramsRu.Add(new KeyValuePair<UnitType, LocalSpace>(UnitType.Flat, new LocalSpace("Квартира")));
            paramsRu.Add(new KeyValuePair<UnitType, LocalSpace>(UnitType.Garage, new LocalSpace("Гараж")));
            paramsRu.Add(new KeyValuePair<UnitType, LocalSpace>(UnitType.Guesthouse, new LocalSpace("Пансионат")));
            paramsRu.Add(new KeyValuePair<UnitType, LocalSpace>(UnitType.Hotel, new LocalSpace("Отель")));
            paramsRu.Add(new KeyValuePair<UnitType, LocalSpace>(UnitType.House, new LocalSpace("Дом")));
            paramsRu.Add(new KeyValuePair<UnitType, LocalSpace>(UnitType.HouseholdServices, new LocalSpace("Бытовые услуги")));
            paramsRu.Add(new KeyValuePair<UnitType, LocalSpace>(UnitType.Industry, new LocalSpace("Производство")));
            paramsRu.Add(new KeyValuePair<UnitType, LocalSpace>(UnitType.Office, new LocalSpace("Офис")));
            paramsRu.Add(new KeyValuePair<UnitType, LocalSpace>(UnitType.Room, new LocalSpace("Комната")));
            paramsRu.Add(new KeyValuePair<UnitType, LocalSpace>(UnitType.PartOfHouse, new LocalSpace("Часть дома")));
            paramsRu.Add(new KeyValuePair<UnitType, LocalSpace>(UnitType.Townhouse, new LocalSpace("Таунхауз")));
            paramsRu.Add(new KeyValuePair<UnitType, LocalSpace>(UnitType.RetailSpace, new LocalSpace("Торговая площадь")));
            paramsRu.Add(new KeyValuePair<UnitType, LocalSpace>(UnitType.Warehouse, new LocalSpace("Склад")));
            paramsRu.Add(new KeyValuePair<UnitType, LocalSpace>(UnitType.ReadyBusiness, new LocalSpace("Готовый бизнес")));


            if (language == Language.Ru)
                return paramsRu.First(kv => kv.Key == unitType).Value;

            return new LocalSpace();
        }

        /// <summary>
        /// Переврдит перечисление в строку для соответствующиго языка
        /// </summary>
        /// <param name="termOfRent">Условия аренды</param>
        /// <param name="language">Язык</param>
        /// <returns></returns>
        public static LocalSpace ToLocalString(this TermOfRent termOfRent, Language language)
        {
            List<KeyValuePair<TermOfRent, LocalSpace>> paramsRu = new List<KeyValuePair<TermOfRent, LocalSpace>>();
            paramsRu.Add(new KeyValuePair<TermOfRent, LocalSpace>(TermOfRent.ChildrenAllowed, new LocalSpace("Можно с детьми")));
            paramsRu.Add(new KeyValuePair<TermOfRent, LocalSpace>(TermOfRent.PetsAllowed, new LocalSpace("Можно с животными")));

            if (language == Language.Ru)
                return paramsRu.First(kv => kv.Key == termOfRent).Value;

            return new LocalSpace();
        }

        /// <summary>
        /// Переврдит перечисление в строку для соответствующиго языка
        /// </summary>
        /// <param name="realEstateProperty">Свойства недвижимости</param>
        /// <param name="language">Язык</param>
        /// <returns></returns>
        public static LocalSpace ToLocalString(this UnitProperty realEstateProperty, Language language)
        {
            List<KeyValuePair<UnitProperty, LocalSpace>> paramsRu = new List<KeyValuePair<UnitProperty, LocalSpace>>();
            paramsRu.Add(new KeyValuePair<UnitProperty, LocalSpace>(UnitProperty.Conditioner, new LocalSpace("Кондиционер")));
            paramsRu.Add(new KeyValuePair<UnitProperty, LocalSpace>(UnitProperty.FirstTime, new LocalSpace("Сдаётся впервые")));
            paramsRu.Add(new KeyValuePair<UnitProperty, LocalSpace>(UnitProperty.Parking, new LocalSpace("Машиноместо")));
            paramsRu.Add(new KeyValuePair<UnitProperty, LocalSpace>(UnitProperty.Loggia, new LocalSpace("Балкон")));
            paramsRu.Add(new KeyValuePair<UnitProperty, LocalSpace>(UnitProperty.Unfurnished, new LocalSpace("Без мебели")));
            paramsRu.Add(new KeyValuePair<UnitProperty, LocalSpace>(UnitProperty.FreshRepair, new LocalSpace("Свежий ремонт")));
            paramsRu.Add(new KeyValuePair<UnitProperty, LocalSpace>(UnitProperty.SeparateBathroom, new LocalSpace("Раздельный санузел")));
            paramsRu.Add(new KeyValuePair<UnitProperty, LocalSpace>(UnitProperty.StudioLayout, new LocalSpace("Студийная планировка")));
            paramsRu.Add(new KeyValuePair<UnitProperty, LocalSpace>(UnitProperty.Lift, new LocalSpace("Лифт")));
            paramsRu.Add(new KeyValuePair<UnitProperty, LocalSpace>(UnitProperty.FencedArea, new LocalSpace("Огороженная територия")));
            paramsRu.Add(new KeyValuePair<UnitProperty, LocalSpace>(UnitProperty.KitchenFurnitur, new LocalSpace("Кухонная мебель")));
            paramsRu.Add(new KeyValuePair<UnitProperty, LocalSpace>(UnitProperty.WashingMachine, new LocalSpace("Стиральная машина")));
            paramsRu.Add(new KeyValuePair<UnitProperty, LocalSpace>(UnitProperty.Dishwasher, new LocalSpace("Посудомоечная машина")));
            paramsRu.Add(new KeyValuePair<UnitProperty, LocalSpace>(UnitProperty.Fridge, new LocalSpace("Холодильник")));
            paramsRu.Add(new KeyValuePair<UnitProperty, LocalSpace>(UnitProperty.Phone, new LocalSpace("Телефон")));
            paramsRu.Add(new KeyValuePair<UnitProperty, LocalSpace>(UnitProperty.Internet, new LocalSpace("Интернет")));
            paramsRu.Add(new KeyValuePair<UnitProperty, LocalSpace>(UnitProperty.Tv, new LocalSpace("Телевизор")));

            if (language == Language.Ru)
                return paramsRu.First(kv => kv.Key == realEstateProperty).Value;

            return new LocalSpace();
        }

        /// <summary>
        /// Переврдит перечисление в строку для соответствующиго языка
        /// </summary>
        /// <param name="contractType">Тип договора</param>
        /// <param name="language">Язык</param>
        /// <returns></returns>
        public static LocalSpace ToLocalString(this ContractType contractType, Language language)
        {
            List<KeyValuePair<ContractType, LocalSpace>> paramsRu = new List<KeyValuePair<ContractType, LocalSpace>>();
            paramsRu.Add(new KeyValuePair<ContractType, LocalSpace>(ContractType.AssignmentOfClaims, new LocalSpace("Договор уступки прав требований")));
            paramsRu.Add(new KeyValuePair<ContractType, LocalSpace>(ContractType.ContractOfSale, new LocalSpace("Договор купли\\продажи")));
            paramsRu.Add(new KeyValuePair<ContractType, LocalSpace>(ContractType.DU214F3, new LocalSpace("Закон 214 ФЗ")));
            paramsRu.Add(new KeyValuePair<ContractType, LocalSpace>(ContractType.GSK, new LocalSpace("Договор ЖСК")));
            paramsRu.Add(new KeyValuePair<ContractType, LocalSpace>(ContractType.InvestmentAgreement, new LocalSpace("Договор инвестирования")));
            paramsRu.Add(new KeyValuePair<ContractType, LocalSpace>(ContractType.JoinAgreement, new LocalSpace("Договор совместной деятельности")));
            paramsRu.Add(new KeyValuePair<ContractType, LocalSpace>(ContractType.SubRent, new LocalSpace("Суб аренда")));
            paramsRu.Add(new KeyValuePair<ContractType, LocalSpace>(ContractType.VideoRent, new LocalSpace("Прямая аренда")));

            if (language == Language.Ru)
                return paramsRu.First(kv => kv.Key == contractType).Value;

            return new LocalSpace();
        }

        /// <summary>
        /// Переврдит перечисление в строку для соответствующиго языка
        /// </summary>
        /// <param name="oldType">Тип возраста недвижимости</param>
        /// <param name="language">Язык</param>
        /// <returns></returns>
        public static LocalSpace ToLocalString(this OldType oldType, Language language)
        {
            List<KeyValuePair<OldType, LocalSpace>> paramsRu = new List<KeyValuePair<OldType, LocalSpace>>();
            paramsRu.Add(new KeyValuePair<OldType, LocalSpace>(OldType.New, new LocalSpace("Новостройка")));
            paramsRu.Add(new KeyValuePair<OldType, LocalSpace>(OldType.Resale, new LocalSpace("Вторичное жилье")));

            if (language == Language.Ru)
                return paramsRu.First(kv => kv.Key == oldType).Value;

            return new LocalSpace();
        }

        /// <summary>
        /// Переврдит перечисление в строку для соответствующиго языка
        /// </summary>
        /// <param name="houseType">Тип дома</param>
        /// <param name="language">Язык</param>
        /// <returns></returns>
        public static LocalSpace ToLocalString(this HouseType houseType, Language language)
        {
            List<KeyValuePair<HouseType, LocalSpace>> paramsRu = new List<KeyValuePair<HouseType, LocalSpace>>();
            paramsRu.Add(new KeyValuePair<HouseType, LocalSpace>(HouseType.Block, new LocalSpace("Блочный")));
            paramsRu.Add(new KeyValuePair<HouseType, LocalSpace>(HouseType.Brick, new LocalSpace("Кирпичный")));
            paramsRu.Add(new KeyValuePair<HouseType, LocalSpace>(HouseType.Monolithic, new LocalSpace("Монолитный")));
            paramsRu.Add(new KeyValuePair<HouseType, LocalSpace>(HouseType.MonolithicBrick, new LocalSpace("Монолитно-кирпичный")));
            paramsRu.Add(new KeyValuePair<HouseType, LocalSpace>(HouseType.Panel, new LocalSpace("Панельный")));
            paramsRu.Add(new KeyValuePair<HouseType, LocalSpace>(HouseType.Stalin, new LocalSpace("Сталинский")));
            paramsRu.Add(new KeyValuePair<HouseType, LocalSpace>(HouseType.Wooden, new LocalSpace("Деревянный")));

            if (language == Language.Ru)
                return paramsRu.First(kv => kv.Key == houseType).Value;

            return new LocalSpace();
        }

        /// <summary>
        /// Переврдит перечисление в строку для соответствующиго языка
        /// </summary>
        /// <param name="utilityPayment">Комунальные услуги</param>
        /// <param name="language">Язык</param>
        /// <returns></returns>
        public static LocalSpace ToLocalString(this UtilityPayment utilityPayment, Language language)
        {
            List<KeyValuePair<UtilityPayment, LocalSpace>> paramsRu = new List<KeyValuePair<UtilityPayment, LocalSpace>>();
            paramsRu.Add(new KeyValuePair<UtilityPayment, LocalSpace>(UtilityPayment.AllInclusive, new LocalSpace("Все включено")));
            paramsRu.Add(new KeyValuePair<UtilityPayment, LocalSpace>(UtilityPayment.AllInclusiveWithoutMeters, new LocalSpace("Все включено без счетчиков")));
            paramsRu.Add(new KeyValuePair<UtilityPayment, LocalSpace>(UtilityPayment.NotIncluded, new LocalSpace("Комунальные услуги не включены")));

            if (language == Language.Ru)
                return paramsRu.First(kv => kv.Key == utilityPayment).Value;

            return new LocalSpace();
        }
    }
}
