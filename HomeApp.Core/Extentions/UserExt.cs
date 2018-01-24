using System;
using System.Text;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Db.Entities.Models;
using HomeApp.Core.Db.Entities.Models.Enums;

namespace HomeApp.Core.Extentions
{
    /// <summary>
    /// Расширения для работы с пользователями
    /// </summary>
    public static class UserExt
    {
        /// <summary>
        /// Преобразует пользователя (базовый класс) в риэлтора
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns></returns>
        public static Realtor ToRealtor(this User user)
        {
            Realtor realtor = new Realtor
            {
                UserType = UserType.Realtor,
                Id = user.Id,
                Membership = user.Membership,
                AdId = user.AdId,
                Address = user.Address,
                Comments = user.Comments,
                Language = user.Language,
                Phones = user.Phones,
                PhotoMinPath = user.PhotoMinPath,
                UserMode = user.UserMode,
                PhotoPath = user.PhotoPath,
                Seo = user.Seo,
                SocialMedia = user.SocialMedia
            };
            return realtor;
        }

        public static Person ToPerson(this User user)
        {
            Person person = new Person
            {
                UserType = UserType.Person,
                Id = user.Id,
                Membership = user.Membership,
                AdId = user.AdId,
                Address = user.Address,
                Comments = user.Comments,
                Language = user.Language,
                Phones = user.Phones,
                PhotoMinPath = user.PhotoMinPath,
                UserMode = user.UserMode,
                PhotoPath = user.PhotoPath,
                Seo = user.Seo,
                SocialMedia = user.SocialMedia
            };
            return person;
        }

        /// <summary>
        /// Переводит объект адреса в строку
        /// </summary>
        /// <param name="address">Объект адреса</param>
        /// <param name="language">Язык</param>
        /// <returns></returns>
        public static string ToStr(this Address address, Language language)
        {
            if (language == Language.Ru)
            {
                StringBuilder sb = new StringBuilder();

                if (address.Country != null)
                    sb.Append($"{address.Country.Ru}, ");
                if (address.Region != null)
                    sb.Append($"{address.Region.Ru}, ");
                if (address.Area != null)
                    sb.Append($"{address.Area.Ru}, ");
                if (address.Sity != null)
                    sb.Append($"{address.Sity.Ru}, ");
                if (address.Street != null)
                    sb.Append($"{address.Street.Ru}, ");
                if (address.StreetNumber != null)
                    sb.Append($"{address.StreetNumber.Ru}, ");
                if (address.Metro != null)
                    sb.Append($"{address.Metro.Ru}, ");

                string addr = sb.ToString().Remove(sb.Length - 2, 2);
                return addr;
            }

            return string.Empty;
        }
    }
}
