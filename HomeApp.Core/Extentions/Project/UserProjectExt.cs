using System;
using System.Collections.Generic;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Db.Entities.Models;
using HomeApp.Core.Extentions.Project.Models.Enums;

namespace HomeApp.Core.Extentions.Project
{
    /// <summary>
    /// Проекция для пользователей
    /// </summary>
    public static class UserProjectExt
    {
        /// <summary>
        /// Проекция для коллекции пользователей
        /// </summary>
        /// <param name="users">Коллекция пользователей</param>
        /// <param name="settings">Настройки проекции для пользователя</param>
        /// <returns></returns>
        public static List<User> Project(this List<User> users, UserProjectSettings settings)
        {
            if (settings == UserProjectSettings.Default)
            {
                List<User> projList = new List<User>();
                foreach (User user in users)
                {
                    if (user is Professional)
                    {
                        Professional professional = new Professional()
                        {
                            Id = user.Id,
                            UserType = user.UserType,
                            UserMode = user.UserMode,
                            AdId = user.AdId,
                            Name = ((Professional) user).Name,
                            Phones = user.Phones,
                            Membership = new Membership
                            {
                                Login = user.Membership.Login,
                                Email = user.Membership.Email
                            },
                            SocialMedia = user.SocialMedia,
                            PhotoMinPath = user.PhotoMinPath,
                            PhotoPath = user.PhotoPath,
                            Language = user.Language,
                            Seo = user.Seo,
                            Address = user.Address,
                        };

                        projList.Add(professional);
                    }
                    else if (user is Person)
                    {
                        Person person = new Person()
                        {
                            Id = user.Id,
                            UserType = user.UserType,
                            UserMode = user.UserMode,
                            AdId = user.AdId,
                            FirstName = ((Person)user).FirstName,
                            LastName = ((Person)user).LastName,
                            MidName = ((Person)user).MidName,
                            Phones = user.Phones,
                            Membership = new Membership
                            {
                                Login = user.Membership.Login,
                                Email = user.Membership.Email
                            },
                            SocialMedia = user.SocialMedia,
                            PhotoMinPath = user.PhotoMinPath,
                            PhotoPath = user.PhotoPath,
                            Language = user.Language,
                            Seo = user.Seo,
                            Address = user.Address,
                        };

                        projList.Add(person);
                    }
                }

                return projList;
            }

            return new List<User>();
        }

        /// <summary>
        /// Проекция для пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="settings">Настройки проекции для пользователя</param>
        /// <returns></returns>
        public static User Project(this User user, UserProjectSettings settings)
        {
            if (settings == UserProjectSettings.Default)
            {
                return user;
            }

            return new User();
        }
    }
}
