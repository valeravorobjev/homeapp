using System.Collections.Generic;
using System.Threading.Tasks;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Db.Entities.Models.Enums;
using HomeApp.Core.Extentions.Filters.Models;
using HomeApp.Core.Extentions.Sorts.Models.Enums;
using HomeApp.Core.Models;
using MongoDB.Bson;

namespace HomeApp.Core.Repositories.Contracts
{
    /// <summary>
    /// Работа с недвижимостью
    /// </summary>
    public interface IRealEstateRepository
    {
        /// <summary>
        /// Отобрать оъекты недвижимости. Объекты будут содержать мимимум полей, необходимых для
        /// отображения в списках и на карте
        /// </summary>
        /// <param name="filter">Фильтер</param>
        /// <param name="sortType">Тпи сортировки</param>
        /// <returns>Возвращает объект RealEstateWrapper</returns>
        Task<RealEstatesModel> GetRealEstates(UnitBaseFilter filter, RealEstateSort sortType);

        /// <summary>
        /// Возвращает топ объектов недвижимости
        /// </summary>
        /// <param name="operationTypes">Типы операций</param>
        /// <param name="take">Сколько записей взять</param>
        /// <returns></returns>
        Task<RealEstatesModel> GetTopRealEstates(List<OperationType> operationTypes, int take);

        /// <summary>
        /// Детальная информация по объекту недвижимости. Объект содержит все имеющиеся поля
        /// </summary>
        /// <param name="realEstateId">Идентификатор недвижимости</param>
        /// <returns></returns>
        Task<RealEstate> GetRealEstate(ObjectId realEstateId);
        /// <summary>
        /// Возвращает количество недвижимости
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <returns></returns>
        Task<int> RealEstateCount(UnitBaseFilter filter);
    }
}
