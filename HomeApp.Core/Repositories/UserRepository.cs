using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeApp.Core.Db;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Db.Entities.Models;
using HomeApp.Core.Db.Entities.Models.Enums;
using HomeApp.Core.Extentions;
using HomeApp.Core.Extentions.Filters;
using HomeApp.Core.Extentions.Filters.Models;
using HomeApp.Core.Extentions.Project;
using HomeApp.Core.Extentions.Project.Models.Enums;
using HomeApp.Core.Extentions.Sorts;
using HomeApp.Core.Extentions.Sorts.Models.Enums;
using HomeApp.Core.Models;
using HomeApp.Core.Repositories.Contracts;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace HomeApp.Core.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _usersCollection;
        private readonly IMongoCollection<RealEstate> _realEstateCollection;

        /// <summary>
        /// Конструктор
        /// </summary>
        public UserRepository(string con)
        {
            IMongoClient client = new MongoClient(con);

            IMongoDatabase db = client.GetDatabase(DbSet.DB_NAME);
            _usersCollection = db.GetCollection<User>(DbSet.USERS_COLLECTION);
            _realEstateCollection = db.GetCollection<RealEstate>(DbSet.REAL_ESTATES_COLLECTION);
        }

        public async Task<User> GetUserAsync(ObjectId userId)
        {
            User user = await _usersCollection.AsQueryable().FirstOrDefaultAsync(u => u.Id == userId);
            return user.Project(UserProjectSettings.Default);
        }

        public async Task<User> GetUserAsync(string login)
        {
            User user = await _usersCollection.AsQueryable().FirstOrDefaultAsync(u => u.Membership.Login == login);
            return user.Project(UserProjectSettings.Default);
        }

        public async Task<UsersModel> GetUsersAsync(UserFilter filter, UserSort sortType)
        {
            List<User> users = await
                _usersCollection.AsQueryable()
                    .Filter(filter)
                    .Sort(sortType)
                    .Skip(filter.Skip)
                    .Take(filter.Take)
                    .ToListAsync();

            int count = await _usersCollection.AsQueryable().Filter(filter).CountAsync();

            return new UsersModel
            {
                Count = count,
                Users = users.Project(UserProjectSettings.Default)
            };
        }

        public async Task<UsersModel> GetProfessionalsAsync(ProfessionalFilter filter, ProfessionalSort sortType)
        {
            IMongoQueryable<User> query = _usersCollection.AsQueryable().Filter(filter);

            List<User> users = await query
                    .Sort(sortType)
                    .Skip(filter.Skip)
                    .Take(filter.Take)
                    .ToListAsync();

            int count = await query.CountAsync();

            return new UsersModel
            {
                Count = count,
                Users = users.Project(UserProjectSettings.Default)
            };
        }

        public async Task<UsersModel> GetTopUsersAsync(List<UserType> userTypes, int take)
        {

            List<User> users = await
                _usersCollection.AsQueryable()
                    .Filter(new UserFilter { UserTypes = userTypes })
                    .OrderByDescending(u => u.Seo.Rating)
                    .ThenByDescending(u => u.Seo.ShowCount)
                    .Take(take)
                    .ToListAsync();

            int count = await _usersCollection.AsQueryable()
                .Filter(new UserFilter { UserTypes = userTypes })
                .Take(take).CountAsync();


            return new UsersModel
            {
                Count = count,
                Users = users.Project(UserProjectSettings.Default)
            };
        }

        public async Task<UsersWithRealEstateCountModel> GetTopUsersWithRealEstateCountAsync(List<UserType> userTypes, int take)
        {
            IMongoQueryable<UserWithRealEstateCountModel> joined = _usersCollection.AsQueryable()
                .Filter(new UserFilter { UserTypes = userTypes })
                .GroupJoin(_realEstateCollection, u => u.Id, r => r.UserId, (user, real) =>
                        new UserWithRealEstateCountModel()
                        {
                            User = new User
                            {
                                Id = user.Id,
                                Address = user.Address,
                                AdId = user.AdId,
                                Language = user.Language,
                                Membership =
                                    new Membership { Login = user.Membership.Login, Email = user.Membership.Email },
                                PhotoPath = user.PhotoPath,
                                PhotoMinPath = user.PhotoMinPath,
                                Phones = user.Phones,
                                Seo = user.Seo,
                                SocialMedia = user.SocialMedia

                            },
                            RealEstateCount = real.Count(
                                r =>
                                    r.RealEstateStatus != RealEstateStatus.Expired &&
                                    r.RealEstateStatus != RealEstateStatus.RemovedByUser
                            )
                        }
                )
                .Where(ur => ur.RealEstateCount > 0) // Отбираем пользователей с объявлениями
                .OrderByDescending(ur => ur.User.Seo.Rating)  // Сортируем по рейтингу
                .ThenByDescending(ur => ur.User.Seo.ShowCount) // Сортируем по количеству просмотров
                .ThenByDescending(ur => ur.RealEstateCount) // Сортируем по количеству объявлений
                .Take(take);

            int count = await joined.CountAsync();
            List<UserWithRealEstateCountModel> userWithRealEstateCount = await joined.ToListAsync();

            UsersWithRealEstateCountModel ul = new UsersWithRealEstateCountModel();
            ul.Count = count;
            ul.UsersWithRealEstateCount = userWithRealEstateCount.Select(
                uwr => new UserWithRealEstateCountModel { RealEstateCount = uwr.RealEstateCount, User = uwr.User.Project(UserProjectSettings.Default) }).ToList();

            return ul;

        }

        public async Task<UsersModel> GetPersonProfessionalsAsync(PersonProfessionalFilter filter, PersonProfessionalSort sortType)
        {
            List<User> users =
                await _usersCollection.AsQueryable()
                    .Filter(filter)
                    .Sort(sortType)
                    .Skip(filter.Skip)
                    .Take(filter.Take)
                    .ToListAsync();

            int count = await _usersCollection.AsQueryable().Filter(filter).CountAsync();


            return new UsersModel()
            {
                Count = count,
                Users = users
            };
        }

        public async Task<CommentsModel> GetCommentsAsync(ObjectId userId, int skip, int take)
        {
            int count =
                await _usersCollection.AsQueryable()
                    .Where(u => u.Id == userId && u.Comments != null)
                    .Select(u => u.Comments.Count)
                    .FirstOrDefaultAsync();

            if (count == 0)
            {
                return new CommentsModel { Count = 0, UserComments = new List<UserCommentModel>() };
            }

            List<Comment> comments = await _usersCollection.AsQueryable().Where(u => u.Id == userId && u.Comments != null)
                .SelectMany(u => u.Comments).Skip(skip).Take(take).ToListAsync();

            CommentsModel commentList = new CommentsModel
            {
                Count = count,
                UserComments = comments.Select(c => new UserCommentModel()
                {
                    Comment = c,
                    User = _usersCollection.AsQueryable().FirstOrDefault(u => u.Id == c.UserId).Project(UserProjectSettings.Default)
                }).ToList()
            };
            return commentList;
        }

        public async Task SetUserTypeAsync(ObjectId userId, UserType userType)
        {
            FilterDefinitionBuilder<User> fb = new FilterDefinitionBuilder<User>();

            User user = await _usersCollection.AsQueryable().FirstOrDefaultAsync(u => u.Id == userId);

            if (userType == UserType.Realtor)
            {
                user = user.ToRealtor();
            }
            else if (userType == UserType.Person)
            {
                user = user.ToPerson();
            }
            else
            {
                throw new Exception("UserType exception");
            }

            await _usersCollection.ReplaceOneAsync(fb.Eq(u => u.Id, userId), user);
        }

        public async Task SetPersonAsync(ObjectId userId, Person person)
        {
            FilterDefinitionBuilder<User> fb = new FilterDefinitionBuilder<User>();

            Person user = (Person) await _usersCollection.AsQueryable().FirstOrDefaultAsync(u => u.Id == userId);
            user.FirstName = person.FirstName;
            user.LastName = person.LastName;
            user.MidName = person.MidName;
            user.DateBirth = person.DateBirth;
            user.Sex = person.Sex;

            await _usersCollection.ReplaceOneAsync(fb.Eq(u => u.Id, userId), user);

        }

        public async Task SetPersonProfessionalAsync(ObjectId userId, PersonProfessional personProfessional)
        {
            FilterDefinitionBuilder<User> fb = new FilterDefinitionBuilder<User>();

            PersonProfessional user = (PersonProfessional)await _usersCollection.AsQueryable().FirstOrDefaultAsync(u => u.Id == userId);

            user.Description = personProfessional.Description;
            user.Education = personProfessional.Education;
            user.FromYear = personProfessional.FromYear;
            user.Site = personProfessional.Site;
            user.Specialization = personProfessional.Specialization;
            user.WorkRegions = personProfessional.WorkRegions;

            await _usersCollection.ReplaceOneAsync(fb.Eq(u => u.Id, userId), user);
        }
    }
}
