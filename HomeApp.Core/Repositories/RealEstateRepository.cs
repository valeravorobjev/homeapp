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

namespace HomeApp.Core.Repositories
{
    /// <summary>
    /// Работа с объектами недвижимости
    /// </summary>
    public class RealEstateRepository : IRealEstateRepository
    {
        private readonly IMongoCollection<RealEstate> _realEstateCollection;
        private readonly IMongoCollection<User> _userCollection;

        /// <summary>
        /// Работа с объектами недвижимости
        /// </summary>
        public RealEstateRepository(string con = null)
        {
            IMongoClient client = new MongoClient(con);
            IMongoDatabase db = client.GetDatabase(DbSet.DB_NAME);
            _realEstateCollection = db.GetCollection<RealEstate>(DbSet.REAL_ESTATES_COLLECTION);
            _userCollection = db.GetCollection<User>(DbSet.USERS_COLLECTION);
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

    }
}
