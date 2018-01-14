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
    }
}
