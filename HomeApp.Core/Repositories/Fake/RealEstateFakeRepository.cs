using System;
using System.Collections.Generic;
using System.Linq;
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

namespace HomeApp.Core.Repositories.Fake
{
    /// <summary>
    /// Работа с объектами недвижимости
    /// </summary>
    public class RealEstateFakeRepository : IRealEstateRepository
    {
        private readonly IMongoCollection<RealEstate> _realEstateCollection;
        private readonly IMongoCollection<User> _userCollection;

        /// <summary>
        /// Работа с объектами недвижимости
        /// </summary>
        public RealEstateFakeRepository(string con = null)
        {
            IMongoClient client = new MongoClient(con);
            IMongoDatabase db = client.GetDatabase(DbSet.DB_NAME_FAKE);
            _realEstateCollection = db.GetCollection<RealEstate>(DbSet.REAL_ESTATES_COLLECTION);
            _userCollection = db.GetCollection<User>(DbSet.USERS_COLLECTION);

            FlatListInit();
        }

        async Task<RealEstatesModel> IRealEstateRepository.GetRealEstates(UnitBaseFilter filter, RealEstateSort sortType)
        {
            IMongoQueryable<RealEstate> query = _realEstateCollection.AsQueryable();
            if (filter is HouseFilter)
            {
                query = query.Filter((HouseFilter)filter);
            }
            else if (filter is FlatFilter)
            {
                query = query.Filter((FlatFilter)filter);
            }
            else if (filter is RoomFilter)
            {
                query = query.Filter((RoomFilter)filter);
            }
            else if (filter is BedSpaceFilter)
            {
                query = query.Filter((BedSpaceFilter)filter);
            }
            else
            {
                query = query.Filter(filter);
            }

            query = query.Project(RealEstateProjectSettings.Short);

            int count = await query.CountAsync();
            List<RealEstate> realEstates = await query.Sort(sortType).Skip(filter.Skip).Take(filter.Take).ToListAsync();

            return new RealEstatesModel
            {
                Count = count,
                RealEstates = realEstates
            };
        }

        async Task<int> IRealEstateRepository.RealEstateCount(UnitBaseFilter filter)
        {
            IMongoQueryable<RealEstate> realEstates = _realEstateCollection.AsQueryable();

            if (filter is HouseFilter)
                realEstates = realEstates.Filter((HouseFilter)filter);
            else if (filter is FlatFilter)
            {
                realEstates = realEstates.Filter((FlatFilter)filter);
            }
            else if (filter is RoomFilter)
            {
                realEstates = realEstates.Filter((RoomFilter)filter);
            }
            else if (filter is BedSpaceFilter)
            {
                realEstates = realEstates.Filter((BedSpaceFilter)filter);
            }
            else
            {
                realEstates = realEstates.Filter(filter);
            }

            int count = await realEstates.CountAsync();

            return count;
        }

        async Task<RealEstatesModel> IRealEstateRepository.GetTopRealEstates(List<OperationType> operationTypes, int take)
        {
            UnitBaseFilter filter = new UnitBaseFilter();
            filter.OperationTypes = operationTypes;

            List<RealEstate> realEstates = await _realEstateCollection.AsQueryable()
                .Filter(filter)
                .Project(RealEstateProjectSettings.Short)
                .OrderByDescending(r => r.Unit.Seo.Rating).ThenByDescending(r => r.Unit.Seo.ShowCount)
                .Skip(0)
                .Take(take)
                .ToListAsync();


            return new RealEstatesModel()
            {
                Count = realEstates.Count,
                RealEstates = realEstates
            };
        }

        async Task<RealEstate> IRealEstateRepository.GetRealEstate(ObjectId realEstateId)
        {
            RealEstate realEstate = await _realEstateCollection.AsQueryable().FirstOrDefaultAsync(f => f.Id == realEstateId);
            return realEstate;
        }

        private void FlatListInit()
        {
            if (_realEstateCollection.AsQueryable().FirstOrDefault() != null)
                return;

            #region InitData

            List<Address> addresses = new List<Address>
            {
                new Address().GoogleGeoCode("Москва, район Ломоносовский, ул. Крупской, 8К3", Language.Ru, "AIzaSyB2BR-PeKqm5vmlfNwyJkDFfZLKYz7-Igo"),
                new Address().GoogleGeoCode("Москва, район Сокольники, ул. Шумкина, 11А", Language.Ru, "AIzaSyB2BR-PeKqm5vmlfNwyJkDFfZLKYz7-Igo"),
                new Address().GoogleGeoCode("Московская область, Люберецкий район, Люберцы, ул. 8 Марта, 36", Language.Ru, "AIzaSyB2BR-PeKqm5vmlfNwyJkDFfZLKYz7-Igo"),
                new Address().GoogleGeoCode("Московская область, Люберецкий район, Люберцы, Комсомольский просп., 10/1", Language.Ru, "AIzaSyB2BR-PeKqm5vmlfNwyJkDFfZLKYz7-Igo"),
                new Address().GoogleGeoCode("Московская область, Люберецкий район, д. Мотяково", Language.Ru, "AIzaSyB2BR-PeKqm5vmlfNwyJkDFfZLKYz7-Igo"),
                new Address().GoogleGeoCode("Россия, г.Кашира, ул.Новокаширская, д.37", Language.Ru, "AIzaSyB2BR-PeKqm5vmlfNwyJkDFfZLKYz7-Igo"),
            };


            List<RealEstate> flats = new List<RealEstate>();
            Random rnd = new Random();
            List<User> users = _userCollection.AsQueryable().Where(u => u.UserType == UserType.Realtor || u.UserType == UserType.Agency).ToList();

            for (int i = 0; i < 1000; i++)
            {
                RealEstate flat = new RealEstate();
                flat.UserId = users.ElementAt(rnd.Next(0, users?.Count ?? 0)).Id;
                flat.PublishDate = DateTime.UtcNow;
                flat.ExpirationDate = DateTime.UtcNow.AddDays(14);
                flat.RealEstateStatus = (RealEstateStatus)Enum.Parse(typeof(RealEstateStatus), rnd.Next(1, 4).ToString());

                flat.Unit = new Flat();
                ((Flat)flat.Unit).OperationType = (OperationType)Enum.Parse(typeof(OperationType), rnd.Next(1, 5).ToString());
                ((Flat)flat.Unit).UnitType = UnitType.Flat;
                ((Flat)flat.Unit).UnitCategory = UnitCategory.Residential;
                ((Flat)flat.Unit).ContractType = (ContractType)Enum.Parse(typeof(ContractType), rnd.Next(1, 9).ToString());
                ((Flat)flat.Unit).Cost = rnd.Next(60000, 1000000) * 0.25f;
                ((Flat)flat.Unit).CurrencyType = CurrencyType.RUR;
                ((Flat)flat.Unit).CommissionFix = rnd.Next(15000, 300000);
                ((Flat)flat.Unit).CommissionPerc = rnd.Next(5, 50);
                ((Flat)flat.Unit).IsMortgage = Convert.ToBoolean(rnd.Next(0, 2));
                ((Flat)flat.Unit).Address = addresses[rnd.Next(0, addresses.Count - 1)];
                ((Flat)flat.Unit).TermOfRents = new List<TermOfRent> { TermOfRent.ChildrenAllowed, TermOfRent.PetsAllowed };
                ((Flat)flat.Unit).UnitProperties = new List<UnitProperty>
                {
                        (UnitProperty)Enum.Parse(typeof(UnitProperty), rnd.Next(1, 18).ToString()),
                        (UnitProperty)Enum.Parse(typeof(UnitProperty), rnd.Next(1, 18).ToString()),
                        (UnitProperty)Enum.Parse(typeof(UnitProperty), rnd.Next(1, 18).ToString()),
                        (UnitProperty)Enum.Parse(typeof(UnitProperty), rnd.Next(1, 18).ToString()),
                        (UnitProperty)Enum.Parse(typeof(UnitProperty), rnd.Next(1, 18).ToString()),
                        (UnitProperty)Enum.Parse(typeof(UnitProperty), rnd.Next(1, 18).ToString())
                };
                ((Flat)flat.Unit).KitchenArea = (int)rnd.Next(30, 80);
                ((Flat)flat.Unit).Floor = (byte)rnd.Next(1, 10);
                ((Flat)flat.Unit).FreightLiftCount = (int)rnd.Next(1, 5);
                ((Flat)flat.Unit).PassengerLiftCount = (int)rnd.Next(1, 5);
                ((Flat)flat.Unit).LivingArea = (int)rnd.Next(40, 200);
                ((Flat)flat.Unit).TotalArea = (int)rnd.Next(80, 300);
                ((Flat)flat.Unit).OldType = (OldType)Enum.Parse(typeof(OldType), rnd.Next(1, 3).ToString());
                ((Flat)flat.Unit).HouseType = (HouseType)Enum.Parse(typeof(HouseType), rnd.Next(1, 8).ToString());
                ((Flat)flat.Unit).Pledge = rnd.Next(0, 100000);
                ((Flat)flat.Unit).PrepaymentTerm = (byte)rnd.Next(0, 3);
                ((Flat)flat.Unit).UtilityPayment = UtilityPayment.AllInclusiveWithoutMeters;
                ((Flat)flat.Unit).RoomCount = (byte)rnd.Next(1, 6);
                ((Flat)flat.Unit).RoomAreas = new List<int>();
                for (int j = 0; j < ((Flat)flat.Unit).RoomCount; j++)
                {
                    ((Flat)flat.Unit).RoomAreas.Add((int)rnd.Next(40, 200));
                }
                ((Flat)flat.Unit).BathroomCount = (byte)rnd.Next(0, 3);
                ((Flat)flat.Unit).CombinedBathroomCount = (byte)rnd.Next(0, 3);
                ((Flat)flat.Unit).CeilingHeight = rnd.Next(0, 10);
                flat.Unit.Description = new Element
                {
                    Ru = @"МФК Савеловский Сити - эксклюзивное предложение! - новый жилой комплекс бизнес-класса. 
                           Предлагается 2х-комнатная квартира общей площадью - 53,9 кв.м., располагающаяся на 28 этаже корпуса в 
                           башне Coltrane (С1). В доме завершается процесс остекления и внешней отделки, в квартирах проведены коммуникации. 
                           Отличные видовые характеристики. Отдельный плюс квартиры уютный балкончик. Раздельный санузел. Действует СКИДКА 3%. 
                           Беспроцентная рассрочка до конца 2017 года. Ипотека от 13,2% годовых. ГК III квартал 2017 года.
                           МФК Савеловский Сити представляет собой монолитное строение и состоит из 6 высоток переменной этажности. 
                           Интересные архитектурные решения удачно сочетаются с современными технологиями строительства и инженерной начинкой.
                           В домах проведена современная система кондиционирования и пожаротушения, установлены скоростные швейцарские лифты Schindler.
                           Многофункциональный Комплекс бизнес-класса расположен на Северо-Востоке столицы, в давно сложившемся Бутырском районе города, 
                           а потому обладает полной инфраструктурой мегаполиса. В шаговой доступности находятся остановки общественного транспорта и две 
                           станции метро Дмитровская и Савеловская. "
                };

                string[] imgs =
                {
                    "/data/fake_img/flat/1.jpg",
                    "/data/fake_img/flat/2.jpg",
                    "/data/fake_img/flat/3.jpg",
                    "/data/fake_img/flat/4.jpg",
                    "/data/fake_img/flat/16.jpg"
                };

                string[] min1 =
{
                    "/data/fake_img/flat/min1/1.jpg",
                    "/data/fake_img/flat/min1/2.jpg",
                    "/data/fake_img/flat/min1/3.jpg",
                    "/data/fake_img/flat/min1/4.jpg",
                    "/data/fake_img/flat/min1/5.jpg",
                    "/data/fake_img/flat/min1/6.jpg",
                    "/data/fake_img/flat/min1/7.jpg",
                    "/data/fake_img/flat/min1/8.jpg",
                    "/data/fake_img/flat/min1/9.jpg",
                    "/data/fake_img/flat/min1/10.jpg",
                    "/data/fake_img/flat/min1/11.jpg",
                    "/data/fake_img/flat/min1/12.jpg",
                    "/data/fake_img/flat/min1/13.jpg",
                    "/data/fake_img/flat/min1/14.jpg",
                    "/data/fake_img/flat/min1/15.jpg",
                    "/data/fake_img/flat/min1/16.jpg"
                };

                string[] min2 =
                {
                    "/data/fake_img/flat/min2/dallas.jpg",
                    "/data/fake_img/flat/min2/denver.jpg",
                    "/data/fake_img/flat/min2/detroit.jpg",
                    "/data/fake_img/flat/min2/los_angeles.jpg",
                    "/data/fake_img/flat/min2/nashville.jpg",
                    "/data/fake_img/flat/min2/palm_bay.jpg",
                    "/data/fake_img/flat/min2/san_diego.jpg",
                    "/data/fake_img/flat/min2/san_fransisco.jpg",
                    "/data/fake_img/flat/min2/san_jose.jpg"
                };

                ((Flat)flat.Unit).PhotoPaths = new List<string>
                {
                    imgs[0],
                    imgs[1],
                    imgs[2],
                    imgs[3],
                    imgs[4]
                };
                ((Flat)flat.Unit).PhotoMinPath400X266 = min1[rnd.Next(0, min1.Length -1)];
                ((Flat)flat.Unit).PhotoMinPath500X500 = min2[rnd.Next(0, min2.Length -1)];
                ((Flat)flat.Unit).Seo = new Seo { LikeCount = rnd.Next(0, 100000), DislikeCount = rnd.Next(0, 1000), Rating = rnd.Next(0, 10), ShowCount = rnd.Next(0, 1000000) };
                flats.Add(flat);
            }
            _realEstateCollection.InsertMany(flats);

            #endregion

            #region Indexes

            _realEstateCollection.Indexes.DropAllAsync();

            List<CreateIndexModel<RealEstate>> indexModels = new List<CreateIndexModel<RealEstate>>
            {
                new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>r.UserId)),
                new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Combine(new List<IndexKeysDefinition<RealEstate>>
                {
                    Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.OperationType),
                    Builders<RealEstate>.IndexKeys.Descending(r=>r.Unit.Seo.Rating),
                    //Builders<RealEstate>.IndexKeys.Descending(r=>r.Unit.Seo.DislikeCount),
                    //Builders<RealEstate>.IndexKeys.Descending(r=>r.Unit.Seo.LikeCount),
                    Builders<RealEstate>.IndexKeys.Descending(r=>r.Unit.Seo.ShowCount)
                }), new CreateIndexOptions {Name = "get_top_real_estates"}),
                new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>r.RealEstateStatus)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>r.PublishDate)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Combine(new List<IndexKeysDefinition<RealEstate>>()
                //{
                //    Builders<RealEstate>.IndexKeys.Geo2D(r=>r.Unit.Address.GeoData),
                //    Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.Address.Country.En),
                //    Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.Address.Country.Ru),
                //    Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.Address.Area.En),
                //    Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.Address.Area.Ru),
                //    Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.Address.Metro.En),
                //    Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.Address.Metro.Ru),
                //    Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.Address.MetroTime),
                //    Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.Address.Region.En),
                //    Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.Address.Region.Ru),
                //    Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.Address.Sity.Ru),
                //    Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.Address.Sity.En),
                //    Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.Address.Street.En),
                //    Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.Address.Street.Ru),
                //    Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.Address.StreetNumber.En),
                //    Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.Address.StreetNumber.Ru)
                //}), new CreateIndexOptions {Name = "address_1"}),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.Advertising.AdId)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.CommissionFix)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.CommissionPerc)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.ContractType)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.Cost)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.CurrencyType)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.IsMortgage)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.OperationType)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.UnitCategory)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.UnitType)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.YearBuilt)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Combine(new List<IndexKeysDefinition<RealEstate>>
                //{
                //    Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.Seo.Rating),
                //    Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.Seo.DislikeCount),
                //    Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.Seo.LikeCount),
                //    Builders<RealEstate>.IndexKeys.Ascending(r=>r.Unit.Seo.ShowCount),
                //}), new CreateIndexOptions {Name = "seo_1"}),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>((BedSpace)r.Unit).Floor)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>((BedSpace)r.Unit).FreightLiftCount)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>((BedSpace)r.Unit).KitchenArea)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>((BedSpace)r.Unit).LivingArea)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>((BedSpace)r.Unit).PassengerLiftCount)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>((BedSpace)r.Unit).TermOfRents)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>((BedSpace)r.Unit).TotalArea)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>((BedSpace)r.Unit).UnitProperties)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>((Room)r.Unit).BathroomCount)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>((Room)r.Unit).CeilingHeight)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>((Room)r.Unit).CombinedBathroomCount)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>((Room)r.Unit).HouseType)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>((Room)r.Unit).Pledge)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>((Room)r.Unit).PrepaymentTerm)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>((Room)r.Unit).RoomAreas)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>((Room)r.Unit).RoomCount)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>((Room)r.Unit).UtilityPayment)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>((Flat)r.Unit).DeliveryDate)),
                //new CreateIndexModel<RealEstate>(Builders<RealEstate>.IndexKeys.Ascending(r=>((Flat)r.Unit).OldType)),
            };

            _realEstateCollection.Indexes.CreateManyAsync(indexModels).Wait();

            #endregion
        }

    }
}
