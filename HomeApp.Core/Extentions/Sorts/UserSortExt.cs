using HomeApp.Core.Db.Entities;
using HomeApp.Core.Db.Entities.Models;
using HomeApp.Core.Extentions.Sorts.Models.Enums;
using MongoDB.Driver.Linq;

namespace HomeApp.Core.Extentions.Sorts
{
    /// <summary>
    /// Методы расширения для сортировки пользователей
    /// </summary>
    public static class UserSortExt
    {
        ///<summary>
        /// Сортирует пользователей
        /// </summary>
        /// <param name="users">Список пользователей</param>
        /// <param name="sortType">Тип сортировки</param>
        /// <returns></returns>
        public static IMongoQueryable<User> Sort(this IMongoQueryable<User> users, PersonSort sortType)
        {
            switch (sortType)
            {
                case PersonSort.FeaturedListings:
                    {
                        users = users.OrderByDescending(u => u.Seo.ShowCount);
                        break;
                    }
                case PersonSort.Rating:
                    {
                        users = users.OrderByDescending(u => u.Seo.Rating);
                        break; ;
                    }
                case PersonSort.Alphabet:
                    {
                        users = users.OrderBy(u => ((PersonProfessional)u).FirstName).ThenBy(u => ((PersonProfessional)u).LastName);
                        break;
                    }
                case PersonSort.DateReg:
                    {
                        users = users.OrderByDescending(u => u.Membership.DateReg);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return users;
        }

        /// <summary>
        /// Сортирует пользователей профессионалов
        /// </summary>
        /// <param name="users">Пользователи</param>
        /// <param name="sortType">Тип сортировки</param>
        /// <returns></returns>
        public static IMongoQueryable<User> Sort(this IMongoQueryable<User> users, PersonProfessionalSort sortType)
        {
            switch (sortType)
            {
                case PersonProfessionalSort.FeaturedListings:
                    {
                        users = users.OrderByDescending(u => u.Seo.ShowCount);
                        break;
                    }
                case PersonProfessionalSort.Rating:
                    {
                        users = users.OrderByDescending(u => u.Seo.Rating);
                        break; ;
                    }
                case PersonProfessionalSort.Alphabet:
                    {
                        users = users.OrderBy(u => ((PersonProfessional)u).FirstName).ThenBy(u => ((PersonProfessional)u).LastName);
                        break;
                    }
                case PersonProfessionalSort.FromYear:
                    {
                        users = users.OrderByDescending(u => ((PersonProfessional)u).FromYear);
                        break;
                    }
                case PersonProfessionalSort.DateReg:
                    {
                        users = users.OrderByDescending(u => u.Membership.DateReg);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return users;
        }

        ///<summary>
        /// Сортирует пользователей
        /// </summary>
        /// <param name="users">Список пользователей</param>
        /// <param name="sortType">Тип сортировки</param>
        /// <returns></returns>
        public static IMongoQueryable<User> Sort(this IMongoQueryable<User> users, ProfessionalSort sortType)
        {
            switch (sortType)
            {
                case ProfessionalSort.FeaturedListings:
                    {
                        users = users.OrderByDescending(u => u.Seo.ShowCount);
                        break;
                    }
                case ProfessionalSort.Alphabet:
                    {
                        users = users.OrderBy(u => ((Professional)u).Name);
                        break;
                    }
                case ProfessionalSort.Rating:
                    {
                        users = users.OrderByDescending(u => u.Seo.Rating);
                        break; ;
                    }
                case ProfessionalSort.FromYear:
                    {
                        users = users.OrderByDescending(u => ((Professional)u).FromYear);
                        break;
                    }
                case ProfessionalSort.DateReg:
                    {
                        users = users.OrderByDescending(u => u.Membership.DateReg);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return users;
        }

        ///<summary>
        /// Сортирует пользователей
        /// </summary>
        /// <param name="users">Список пользователей</param>
        /// <param name="sortType">Тип сортировки</param>
        /// <returns></returns>
        public static IMongoQueryable<User> Sort(this IMongoQueryable<User> users, UserSort sortType)
        {
            switch (sortType)
            {
                default:
                    {
                        break;
                    }
            }

            return users;
        }
    }
}
