using System;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Exceptions;
using HomeApp.Core.Extentions.Filters.Models;
using MongoDB.Driver.Linq;

namespace HomeApp.Core.Extentions.Filters
{
    /// <summary>
    /// Методы расширений для базовых объявлений
    /// </summary>
    public static class UnitBaseFilterExt
    {
        /// <summary>
        /// Фильтр базовых объектов недвижимости
        /// </summary>
        /// <param name="realEstates">Объявления</param>
        /// <param name="filter">Фильтр</param>
        /// <returns></returns>
        public static IMongoQueryable<RealEstate> Filter(this IMongoQueryable<RealEstate> realEstates, UnitBaseFilter filter)
        {
            if (filter == null) throw new InvalidCastException("Filter must be cast to UnitBaseFilter");

            realEstates = realEstates.Filter((RealEstateFilter)filter);

            if (filter.ContractTypes?.Count > 0)
            {
                realEstates = realEstates.Where(r => filter.ContractTypes.Contains(r.Unit.ContractType));
            }
            if (filter.UnitCategories?.Count > 0)
            {
                realEstates = realEstates.Where(r => filter.UnitCategories.Contains(r.Unit.UnitCategory));
            }
            if (filter.OperationTypes?.Count > 0)
            {
                realEstates = realEstates.Where(r => filter.OperationTypes.Contains(r.Unit.OperationType));
            }
            if (filter.UnitTypes?.Count > 0)
            {
                realEstates = realEstates.Where(r => filter.UnitTypes.Contains(r.Unit.UnitType));
            }
            if (filter.Cost != null)
            {
                realEstates = realEstates.Where(r => r.Unit.Cost >= filter.Cost.Low && r.Unit.Cost <= filter.Cost.Hight);
            }
            if (filter.CommissionFix != null)
            {
                realEstates = realEstates.Where(r => r.Unit.CommissionFix >= filter.CommissionFix.Low && r.Unit.CommissionFix <= filter.CommissionFix.Hight);
            }
            if (filter.CommissionPerc != null)
            {
                realEstates = realEstates.Where(r => r.Unit.CommissionPerc >= filter.CommissionPerc.Low && r.Unit.CommissionPerc <= filter.CommissionPerc.Hight);
            }
            if (filter.CurrencyTypes?.Count > 0)
            {
                realEstates = realEstates.Where(r => filter.CurrencyTypes.Contains(r.Unit.CurrencyType));
            }
            if (filter.IsMortgage != null)
            {
                realEstates = realEstates.Where(r => r.Unit.IsMortgage == filter.IsMortgage);
            }
            if (filter.Address != null)
            {
                if (filter.Address.Country != null)
                {
                    string country = filter.Address.Country.En ?? filter.Address.Country.Ru;
                    if (string.IsNullOrEmpty(country)) throw new AddressException("Contry name is null or empty");
                    realEstates = realEstates.Where(r => r.Unit.Address.Country.En == country || r.Unit.Address.Country.Ru == country);
                }
                if (filter.Address.Region != null)
                {
                    string region = filter.Address.Region.En ?? filter.Address.Region.Ru;
                    if (string.IsNullOrEmpty(region)) throw new AddressException("Region name is null or empty");
                    realEstates = realEstates.Where(r => r.Unit.Address.Region.En == region || r.Unit.Address.Region.Ru == region);
                }
                if (filter.Address.Area != null)
                {
                    string area = filter.Address.Area.En ?? filter.Address.Area.Ru;
                    if (string.IsNullOrEmpty(area)) throw new AddressException("Area name is null or empty");
                    realEstates = realEstates.Where(r => r.Unit.Address.Area.En == area || r.Unit.Address.Area.Ru == area);
                }
                if (filter.Address.Sity != null)
                {
                    string sity = filter.Address.Sity.En ?? filter.Address.Sity.Ru;
                    if (string.IsNullOrEmpty(sity)) throw new AddressException("Sity name is null or empty");
                    realEstates = realEstates.Where(r => r.Unit.Address.Sity.En == sity || r.Unit.Address.Sity.Ru == sity);
                }
                if (filter.Address.Street != null)
                {
                    string street = filter.Address.Street.En ?? filter.Address.Street.Ru;
                    if (string.IsNullOrEmpty(street)) throw new AddressException("Street name is null or empty");
                    realEstates = realEstates.Where(r => r.Unit.Address.Street.En == street || r.Unit.Address.Street.Ru == street);
                }
                if (filter.Address.StreetNumber != null)
                {
                    string streetNumber = filter.Address.StreetNumber.En ?? filter.Address.StreetNumber.Ru;
                    if (string.IsNullOrEmpty(streetNumber)) throw new AddressException("StreetNumber name is null or empty");
                    realEstates = realEstates.Where(r => r.Unit.Address.StreetNumber.En == streetNumber || r.Unit.Address.StreetNumber.Ru == streetNumber);
                }
                if (filter.Address.Metro != null)
                {
                    string metro = filter.Address.Metro.En ?? filter.Address.Metro.Ru;
                    if (string.IsNullOrEmpty(metro)) throw new AddressException("Metro name is null or empty");
                    realEstates = realEstates.Where(r => r.Unit.Address.Metro.En == metro || r.Unit.Address.Metro.Ru == metro);
                }
                if (filter.Address.MetroTime > 0)
                {
                    realEstates = realEstates.Where(r => r.Unit.Address.MetroTime == filter.Address.MetroTime);
                }
            }

            if (filter.YearBuilt != null)
            {
                realEstates =
                    realEstates.Where(
                        r => r.Unit.YearBuilt >= filter.YearBuilt.Low && r.Unit.YearBuilt <= filter.YearBuilt.Hight);
            }

            return realEstates;
        }
    }
}