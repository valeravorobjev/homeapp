using System.Collections.Generic;
using HomeApp.Core.Db.Entities.Models;
using HomeApp.Core.Db.Entities.Models.Enums;

namespace HomeApp.Core.Extentions.Filters.Models
{
    /// <summary>
    /// Базовый фильтр для всех объектов недвижимости
    /// </summary>
    public class UnitBaseFilter: RealEstateFilter
    {
        /// <summary>
        /// Типы операции с недвижимостью
        /// </summary>
        public List<OperationType> OperationTypes { get; set; }
        /// <summary>
        /// Категории недвижимости
        /// </summary>
        public List<UnitCategory> UnitCategories { get; set; }
        /// <summary>
        /// Тип договора
        /// </summary>
        public List<ContractType> ContractTypes { get; set; }
        /// <summary>
        /// Типы недвижимости
        /// </summary>
        public List<UnitType> UnitTypes { get; set; }
        /// <summary>
        /// Минимальная стоимость
        /// </summary>
        public Range<float> Cost { get; set; }
        /// <summary>
        /// Типы валют
        /// </summary>
        public List<CurrencyType> CurrencyTypes { get; set; }
        /// <summary>
        /// Коммисия процент. Нижний порог
        /// </summary>
        public Range<float> CommissionPerc { get; set; }
        /// <summary>
        /// Коммисия фиксированная цена. Нижний порог
        /// </summary>
        public Range<float> CommissionFix { get; set; }
        /// <summary>
        /// Возможна эпотека
        /// </summary>
        public bool? IsMortgage { get; set; }
        /// <summary>
        /// Адрес
        /// </summary>
        public Address Address { get; set; }
        /// <summary>
        /// Год постройки
        /// </summary>
        public Range<int> YearBuilt { get; set; }
    }
}
