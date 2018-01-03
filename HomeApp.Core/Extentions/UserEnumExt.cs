using System.Collections.Generic;
using System.Linq;
using HomeApp.Core.Db.Entities.Models.Enums;
using HomeApp.Core.Extentions.Models;

namespace HomeApp.Core.Extentions
{
    /// <summary>
    /// Расширения для перечислений класса User
    /// </summary>
    public static class UserEnumExt
    {

        /// <summary>
        /// Переврдит перечисление в строку для соответствующиго языка
        /// </summary>
        /// <param name="addService">Дополнительные услуги</param>
        /// <param name="language">Язык</param>
        /// <returns></returns>
        public static LocalSpace ToLocalString(this AddService addService, Language language)
        {
            List<KeyValuePair<AddService, LocalSpace>> paramsRu = new List<KeyValuePair<AddService, LocalSpace>>();
            paramsRu.Add(new KeyValuePair<AddService, LocalSpace>(AddService.Advice, new LocalSpace("Консультация")));
            paramsRu.Add(new KeyValuePair<AddService, LocalSpace>(AddService.Credit, new LocalSpace("Кредитование")));
            paramsRu.Add(new KeyValuePair<AddService, LocalSpace>(AddService.Development, new LocalSpace("Строительство")));
            paramsRu.Add(new KeyValuePair<AddService, LocalSpace>(AddService.Mortgage, new LocalSpace("Ипотека")));
            paramsRu.Add(new KeyValuePair<AddService, LocalSpace>(AddService.SearchRealEstate, new LocalSpace("Поиск недвижимости")));

            if (language == Language.Ru)
                return paramsRu.First(kv => kv.Key == addService).Value;

            return new LocalSpace();
        }

        /// <summary>
        /// Переврдит перечисление в строку для соответствующиго языка
        /// </summary>
        /// <param name="userType">Тип пользователя</param>
        /// <param name="language">Язык</param>
        /// <returns></returns>
        public static LocalSpace ToLocalString(this UserType userType, Language language)
        {
            List<KeyValuePair<UserType, LocalSpace>> paramsRu = new List<KeyValuePair<UserType, LocalSpace>>();
            paramsRu.Add(new KeyValuePair<UserType, LocalSpace>(UserType.Agency, new LocalSpace {Single = "Агенство", Plural = "Агенства"}));
            paramsRu.Add(new KeyValuePair<UserType, LocalSpace>(UserType.Developer, new LocalSpace {Single = "Застройщик" , Plural = "Застройщики"}));
            paramsRu.Add(new KeyValuePair<UserType, LocalSpace>(UserType.Person, new LocalSpace {Single = "Пользователь", Plural = "Пользователи"}));
            paramsRu.Add(new KeyValuePair<UserType, LocalSpace>(UserType.Realtor, new LocalSpace {Single = "Риэлтор" , Plural = "Риэлторы"}));

            if (language == Language.Ru)
                return paramsRu.First(kv => kv.Key == userType).Value;

            return new LocalSpace();
        }
    }
}
