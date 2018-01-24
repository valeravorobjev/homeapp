using System.Net.Http;
using HomeApp.Core.Db.Entities.Models;
using HomeApp.Core.Db.Entities.Models.Enums;
using HomeApp.Core.Exceptions;
using HomeApp.Core.Extentions.Models;
using Newtonsoft.Json;
using System.Linq;

namespace HomeApp.Core.Extentions
{
    /// <summary>
    /// Содержит расширения для геокодирования
    /// </summary>
    public static class GeoCodeExt
    {
        /// <summary>
        /// Выполняет геокодирование с помощью google api.
        /// </summary>
        /// <param name="address">Структура для заполнения</param>
        /// <param name="str">Страка с адресом</param>
        /// <param name="language">Язык для геокодирования</param>
        /// <param name="key">Секретный ключ доступа к api</param>
        public static Address GoogleGeoCode(this Address address, string str, Language language, string key)
        {
            if (string.IsNullOrEmpty(str)) return null;

            string lng = "en";
            if (language == Language.Ru)
                lng = "ru";

            string endpoint = "https://maps.googleapis.com/maps/api/geocode/json";
            string url = $"{endpoint}?address={str}&language={lng}&key={key}";

            using (HttpClient client = new HttpClient())
            {
                var json = client.GetStringAsync(url).Result;

                GeoCode geoCode = JsonConvert.DeserializeObject<GeoCode>(json);

                if (geoCode.Status != "OK")
                    throw new GeoCodeException("Can't geocode with google api!!!");

                Result result = geoCode.Results.FirstOrDefault();

                if (result == null)
                    throw new GeoCodeException("Not found");
                if (address == null) address = new Address();

                address.GeoData = new GeoData();
                address.GeoData.Latitude = result.Geometry.Location.Lat;
                address.GeoData.Longitude = result.Geometry.Location.Lng;

                foreach (AddressComponent addr in result.AddressComponents)
                {
                    if (addr.Types.Any(t => t == "country"))
                    {
                        address.Country = new Element();

                        if (language == Language.Ru)
                            address.Country.Ru = addr.LongName;
                        else if (language == Language.En)
                            address.Country.En = addr.LongName;
                    }
                    else if (addr.Types.Any(t => t == "administrative_area_level_1"))
                    {
                        address.Region = new Element();

                        if (language == Language.Ru)
                            address.Region.Ru = addr.LongName;
                        else if (language == Language.En)
                            address.Region.En = addr.LongName;
                    }
                    else if (addr.Types.Any(t => t == "administrative_area_level_2"))
                    {
                        address.Area = new Element();

                        if (language == Language.Ru)
                            address.Area.Ru = addr.LongName;
                        else if (language == Language.En)
                            address.Area.En = addr.LongName;
                    }
                    else if (addr.Types.Any(t => t == "locality"))
                    {
                        address.Sity = new Element();

                        if (language == Language.Ru)
                            address.Sity.Ru = addr.LongName;
                        else if (language == Language.En)
                            address.Sity.En = addr.LongName;
                    }
                    else if (addr.Types.Any(t => t == "route"))
                    {
                        address.Street = new Element();

                        if (language == Language.Ru)
                            address.Street.Ru = addr.LongName;
                        else if (language == Language.En)
                            address.Street.En = addr.LongName;
                    }
                    else if (addr.Types.Any(t => t == "street_number"))
                    {
                        address.StreetNumber = new Element();

                        if (language == Language.Ru)
                            address.StreetNumber.Ru = addr.LongName;
                        else if (language == Language.En)
                            address.StreetNumber.En = addr.LongName;
                    }
                }

            }

            return address;
        }
    }
}
