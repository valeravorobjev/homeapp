using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Bogus.DataSets;
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
using HomeApp.Core.Repositories.Contracts;
using HomeApp.Core.ViewModels;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace HomeApp.Core.Repositories.Fake
{
    /// <summary>
    /// Работа с пользователем
    /// </summary>
    public class UserFakeRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _usersCollection;
        private readonly IMongoCollection<RealEstate> _realEstateCollection;

        /// <summary>
        /// Конструктор
        /// </summary>
        public UserFakeRepository(string con)
        {
            IMongoClient client = new MongoClient(con);
            
            IMongoDatabase db = client.GetDatabase(DbSet.DB_NAME_FAKE);
            _usersCollection = db.GetCollection<User>(DbSet.USERS_COLLECTION);
            _realEstateCollection = db.GetCollection<RealEstate>(DbSet.REAL_ESTATES_COLLECTION);


            UsersInit();
        }

        async Task<User> IUserRepository.GetUser(ObjectId userId)
        {
            User user = await _usersCollection.AsQueryable().FirstOrDefaultAsync(u => u.Id == userId);
            return user.Project(UserProjectSettings.Default);
        }

        async Task<UserList> IUserRepository.GetUsers(UserFilter filter, UserSort sortType)
        {
            List<User> users = await
                _usersCollection.AsQueryable()
                    .Filter(filter)
                    .Sort(sortType)
                    .Skip(filter.Skip)
                    .Take(filter.Take)
                    .ToListAsync();

            int count = await _usersCollection.AsQueryable().Filter(filter).CountAsync();

            return new UserList
            {
                Count = count,
                Users = users.Project(UserProjectSettings.Default)
            };
        }

        async Task<UserList> IUserRepository.GetProfessionals(ProfessionalFilter filter, ProfessionalSort sortType)
        {
            IMongoQueryable<User> query = _usersCollection.AsQueryable().Filter(filter);

            List<User> users = await query
                    .Sort(sortType)
                    .Skip(filter.Skip)
                    .Take(filter.Take)
                    .ToListAsync();

            int count = await query.CountAsync();

            return new UserList
            {
                Count = count,
                Users = users.Project(UserProjectSettings.Default)
            };
        }

        async Task<UserList> IUserRepository.GetTopUsers(List<UserType> userTypes, int take)
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


            return new UserList
            {
                Count = count,
                Users = users.Project(UserProjectSettings.Default)
            };
        }

        async Task<UserWithRealEstateCountList> IUserRepository.GetTopUsersWithRealEstateCount(List<UserType> userTypes, int take)
        {
            IMongoQueryable<UserWithRealEstateCount> joined = _usersCollection.AsQueryable()
                .Filter(new UserFilter { UserTypes = userTypes })
                .GroupJoin(_realEstateCollection, u => u.Id, r => r.UserId, (user, real) =>
                        new UserWithRealEstateCount()
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
            List<UserWithRealEstateCount> userWithRealEstateCount = await joined.ToListAsync();

            UserWithRealEstateCountList ul = new UserWithRealEstateCountList();
            ul.Count = count;
            ul.UsersWithRealEstateCount = userWithRealEstateCount.Select(
                uwr => new UserWithRealEstateCount { RealEstateCount = uwr.RealEstateCount, User = uwr.User.Project(UserProjectSettings.Default) }).ToList();

            return ul;

        }

        async Task<UserList> IUserRepository.GetPersonProfessionals(PersonProfessionalFilter filter, PersonProfessionalSort sortType)
        {
            List<User> users =
                await _usersCollection.AsQueryable()
                    .Filter(filter)
                    .Sort(sortType)
                    .Skip(filter.Skip)
                    .Take(filter.Take)
                    .ToListAsync();

            int count = await _usersCollection.AsQueryable().Filter(filter).CountAsync();


            return new UserList()
            {
                Count = count,
                Users = users
            };
        }

        async Task<CommentList> IUserRepository.GetComments(ObjectId userId, int skip, int take)
        {
            int count =
                await _usersCollection.AsQueryable()
                    .Where(u => u.Id == userId && u.Comments != null)
                    .Select(u => u.Comments.Count)
                    .FirstOrDefaultAsync();

            if (count == 0)
            {
                return new CommentList { Count = 0, UserComments = new List<UserComment>() };
            }

            List<Comment> comments = await _usersCollection.AsQueryable().Where(u => u.Id == userId && u.Comments != null)
                .SelectMany(u => u.Comments).Skip(skip).Take(take).ToListAsync();

            CommentList commentList = new CommentList
            {
                Count = count,
                UserComments = comments.Select(c => new UserComment()
                {
                    Comment = c,
                    User = _usersCollection.AsQueryable().FirstOrDefault(u => u.Id == c.UserId).Project(UserProjectSettings.Default)
                }).ToList()
            };
            return commentList;
        }

        private void UsersInit()
        {
            if (_usersCollection.Count(FilterDefinition<User>.Empty) > 0)
                return;

            #region InitData

            List<User> users = new List<User>();

            for (int i = 0; i < 2000; i++)
            {
                Bogus.Person person = new Bogus.Person("ru");
                Name name = new Name();
                Lorem lorem = new Lorem("ru");
                Randomizer random = new Randomizer();
                Internet internet = new Internet("ru");
                Bogus.DataSets.Address address = new Bogus.DataSets.Address("ru");
                Company company = new Company("ru");

                users.Add(new Db.Entities.Models.Person
                {
                    FirstName = new Element { Ru = person.FirstName },
                    LastName = new Element { Ru = person.LastName },
                    DateBirth = person.DateOfBirth,
                    Sex = random.Bool(),
                    UserType = UserType.Person,
                    UserMode = UserMode.Normal,
                    Phones = new List<string> { person.Phone },
                    Membership = new Membership
                    {
                        Login = person.Email,
                        Email = person.Email
                    },
                    SocialMedia = new SocialMedia
                    {
                        Facebook = $"https://www.facebook.com/{person.FirstName}",
                        Twitter = $"https://twitter.com/{person.FirstName}"
                    },
                    PhotoMinPath = internet.Avatar(),
                    Language = Language.Ru,
                    Address = new Db.Entities.Models.Address
                    {
                        Country = new Element { Ru = address.Country() },
                        Region = new Element { Ru = address.State() },
                        Sity = new Element { Ru = address.City() },
                        Street = new Element { Ru = address.StreetName() },
                        GeoData = new GeoData { Latitude = (float)person.Address.Geo.Lat, Longitude = (float)person.Address.Geo.Lng },
                    }
                });
            }


            for (int i = 0; i < 1000; i++)
            {
                Bogus.Person person = new Bogus.Person("ru");
                Name name = new Name();
                Lorem lorem = new Lorem("ru");
                Randomizer random = new Randomizer();
                Internet internet = new Internet("ru");
                Bogus.DataSets.Address address = new Bogus.DataSets.Address("ru");
                Company company = new Company("ru");

                users.Add(new Rialtor
                {
                    UserType = UserType.Realtor,
                    UserMode = random.Enum<UserMode>(),
                    Membership = new Membership
                    {
                        Email = person.Email
                    },
                    SocialMedia = new SocialMedia
                    {
                        Facebook = $"https://www.facebook.com/{person.FirstName}",
                        Twitter = $"https://twitter.com/{person.FirstName}"
                    },
                    FirstName = new Element { Ru = person.FirstName },
                    LastName = new Element { Ru = person.LastName },
                    Education = new Element { Ru = lorem.Paragraphs() },
                    DateBirth = person.DateOfBirth,
                    Sex = random.Bool(),
                    Job = new Element { Ru = name.JobTitle() },
                    Language = Language.Ru,
                    Phones = new List<string> { person.Phone },
                    PhotoMinPath = internet.Avatar(),
                    Site = person.Website,
                    FromYear = (int)random.Number(1990, 2016),
                    WorkRegions = new List<string> { "Москва", "Московская область" },
                    Specialization = new Specialization
                    {
                        EstateSales = new List<UnitCategory> { random.Enum<UnitCategory>(), random.Enum<UnitCategory>() },
                        RentalProperties = new List<UnitCategory> { random.Enum<UnitCategory>(), random.Enum<UnitCategory>() },
                        AddServices = new List<AddService> { AddService.Advice }
                    },
                    Description = new Element { Ru = lorem.Paragraphs(10) },
                    Seo = new Seo { DislikeCount = random.Int(0, 1000), LikeCount = random.Int(0, 1000), ShowCount = random.Int(0, 100000), Rating = random.UInt(0, 5) },
                    Address = new Db.Entities.Models.Address
                    {
                        Country = new Element { Ru = address.Country() },
                        Region = new Element { Ru = address.State() },
                        Sity = new Element { Ru = address.City() },
                        Street = new Element { Ru = address.StreetName() },
                        GeoData = new GeoData { Latitude = (float)person.Address.Geo.Lat, Longitude = (float)person.Address.Geo.Lng },
                    }
                });
            }

            for (int i = 0; i < 1000; i++)
            {
                Bogus.Person person = new Bogus.Person("ru");
                Name name = new Name();
                Lorem lorem = new Lorem("ru");
                Randomizer random = new Randomizer();
                Internet internet = new Internet("ru");
                Bogus.DataSets.Address address = new Bogus.DataSets.Address("ru");
                Company company = new Company("ru");

                users.Add(new Agency
                {
                    UserType = UserType.Agency,
                    UserMode = random.Enum<UserMode>(),
                    Membership = new Membership
                    {
                        Email = person.Email
                    },
                    Name = new Element { Ru = company.CompanyName() },
                    FromYear = (int)random.Number(1990, 2016),
                    WorkRegions = new List<string> { "Москва", "Московская область" },
                    Specialization = new Specialization
                    {
                        EstateSales = new List<UnitCategory> { random.Enum<UnitCategory>(), random.Enum<UnitCategory>() },
                        RentalProperties = new List<UnitCategory> { random.Enum<UnitCategory>(), random.Enum<UnitCategory>() },
                        AddServices = new List<AddService> { AddService.Advice }
                    },
                    Description = new Element { Ru = lorem.Text() },
                    Site = person.Website,
                    Phones = new List<string> { person.Phone },
                    SocialMedia = new SocialMedia
                    {
                        Facebook = $"https://www.facebook.com/{person.FirstName}",
                        Twitter = $"https://twitter.com/{person.FirstName}"
                    },
                    PhotoMinPath = internet.Avatar(),
                    Language = Language.Ru,
                    Seo = new Seo { DislikeCount = random.Int(0, 1000), LikeCount = random.Int(0, 1000), ShowCount = random.Int(0, 100000), Rating = random.Int(0, 5) },
                    Address = new Db.Entities.Models.Address
                    {
                        Country = new Element { Ru = address.Country() },
                        Region = new Element { Ru = address.State() },
                        Sity = new Element { Ru = address.City() },
                        Street = new Element { Ru = address.StreetName() },
                        GeoData = new GeoData { Latitude = (float)person.Address.Geo.Lat, Longitude = (float)person.Address.Geo.Lng },
                    }
                });
            }

            for (int i = 0; i < 500; i++)
            {
                Bogus.Person person = new Bogus.Person("ru");
                Name name = new Name();
                Lorem lorem = new Lorem("ru");
                Randomizer random = new Randomizer();
                Internet internet = new Internet("ru");
                Bogus.DataSets.Address address = new Bogus.DataSets.Address("ru");
                Company company = new Company("ru");

                users.Add(new Developer()
                {
                    UserType = UserType.Developer,
                    UserMode = random.Enum<UserMode>(),
                    Membership = new Membership
                    {
                        Email = person.Email
                    },
                    Name = new Element { Ru = company.CompanyName() },
                    FromYear = (int)random.Number(1990, 2016),
                    WorkRegions = new List<string> { "Москва", "Московская область" },
                    Specialization = new Specialization
                    {
                        EstateSales = new List<UnitCategory> { random.Enum<UnitCategory>(), random.Enum<UnitCategory>() },
                        RentalProperties = new List<UnitCategory> { random.Enum<UnitCategory>(), random.Enum<UnitCategory>() },
                        AddServices = new List<AddService> { AddService.Development }
                    },
                    Description = new Element { Ru = lorem.Text() },
                    Site = person.Website,
                    Phones = new List<string> { person.Phone },
                    SocialMedia = new SocialMedia
                    {
                        Facebook = $"https://www.facebook.com/{person.FirstName}",
                        Twitter = $"https://twitter.com/{person.FirstName}"
                    },
                    PhotoMinPath = internet.Avatar(),
                    Language = Language.Ru,
                    Seo = new Seo { DislikeCount = random.Int(0, 1000), LikeCount = random.Int(0, 1000), ShowCount = random.Int(0, 100000), Rating = random.Int(0, 5) },
                    Address = new Db.Entities.Models.Address
                    {
                        Country = new Element { Ru = address.Country() },
                        Region = new Element { Ru = address.State() },
                        Sity = new Element { Ru = address.City() },
                        Street = new Element { Ru = address.StreetName() },
                        GeoData = new GeoData { Latitude = (float)person.Address.Geo.Lat, Longitude = (float)person.Address.Geo.Lng },
                    }
                });
            }

            foreach (User user in users)
            {
                user.BuildSecure("P@ssword");
            }

            _usersCollection.InsertMany(users);

            {
                Bogus.Person person = new Bogus.Person("ru");
                Name name = new Name();
                Lorem lorem = new Lorem("ru");
                Randomizer random = new Randomizer();
                Internet internet = new Internet("ru");
                Bogus.DataSets.Address address = new Bogus.DataSets.Address("ru");
                Company company = new Company("ru");

                List<User> persons = _usersCollection.AsQueryable()
                    .Where(u => u.UserType == UserType.Person)
                    .ToList();

                List<User> professionals = _usersCollection.AsQueryable()
                    .Where(u => u.UserType == UserType.Realtor || u.UserType == UserType.Agency)
                    .ToList();

                foreach (User professional in professionals)
                {
                    List<Comment> comments = new List<Comment>();
                    for (int j = 0; j < random.Int(10, 100); j++)
                    {
                        comments.Add(new Comment
                        {
                            Date = person.DateOfBirth,
                            Eval = random.Byte(0, 5),
                            Text = lorem.Paragraphs(),
                            UserId = persons.ElementAt(random.Int(0, 1999)).Id
                        });
                    }

                    _usersCollection.UpdateOne(Builders<User>.Filter.Eq(u => u.Id, professional.Id),
                        Builders<User>.Update.PushEach(u => u.Comments, comments));
                }

            }

            #endregion

            #region Indexes

            _usersCollection.Indexes.DropAllAsync().Wait();

            List<CreateIndexModel<User>> indexModels = new List<CreateIndexModel<User>>
            {
                new CreateIndexModel<User>(Builders<User>.IndexKeys.Combine(new List<IndexKeysDefinition<User>>
                {
                    Builders<User>.IndexKeys.Ascending(u=>u.UserType),
                    Builders<User>.IndexKeys.Descending(u=>u.Seo.Rating),
                    //Builders<User>.IndexKeys.Descending(u=>u.Seo.ShowCount),
                    //Builders<User>.IndexKeys.Descending(u=>u.Seo.LikeCount),
                    Builders<User>.IndexKeys.Descending(u=>u.Seo.DislikeCount)
                }), new CreateIndexOptions { Name = "get_top_users_with_realestate_count"}),




                //new CreateIndexModel<User>(Builders<User>.IndexKeys.Ascending(u=>u.UserMode)),
                //new CreateIndexModel<User>(Builders<User>.IndexKeys.Ascending(u=>u.AdId)),
                //new CreateIndexModel<User>(Builders<User>.IndexKeys.Ascending(u=>u.Membership.Email)),
                //new CreateIndexModel<User>(Builders<User>.IndexKeys.Ascending(u=>u.Membership.Login)),
                //new CreateIndexModel<User>(Builders<User>.IndexKeys.Ascending(u=> ((Entities.Models.Person)u).FirstName)),
                //new CreateIndexModel<User>(Builders<User>.IndexKeys.Ascending(u=> ((Entities.Models.Person)u).LastName)),
                //new CreateIndexModel<User>(Builders<User>.IndexKeys.Ascending(u=> ((Entities.Models.Person)u).MidName)),
                //new CreateIndexModel<User>(Builders<User>.IndexKeys.Ascending(u=> ((Entities.Models.Person)u).DateBirth)),
                //new CreateIndexModel<User>(Builders<User>.IndexKeys.Ascending(u=> ((Entities.Models.Person)u).Sex)),
                //new CreateIndexModel<User>(Builders<User>.IndexKeys.Ascending(u=> ((PersonProfessional)u).FromYear)),
                //new CreateIndexModel<User>(Builders<User>.IndexKeys.Ascending(u=> ((PersonProfessional)u).WorkRegions)),
                //new CreateIndexModel<User>(Builders<User>.IndexKeys.Ascending(u=> ((PersonProfessional)u).Specialization.AddServices)),
                //new CreateIndexModel<User>(Builders<User>.IndexKeys.Ascending(u=> ((PersonProfessional)u).Specialization.EstateSales)),
                //new CreateIndexModel<User>(Builders<User>.IndexKeys.Ascending(u=> ((PersonProfessional)u).Specialization.RentalProperties)),
                //new CreateIndexModel<User>(Builders<User>.IndexKeys.Ascending(u=> ((Professional)u).Name)),
            };

            _usersCollection.Indexes.CreateManyAsync(indexModels).Wait();


            #endregion
        }
    }
}

