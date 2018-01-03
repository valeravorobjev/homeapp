using System;
using System.Threading.Tasks;
using HomeApp.Core.Db;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Extentions;
using HomeApp.Core.Repositories.Contracts;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HomeApp.Core.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly IMongoDatabase _database;
        public SessionRepository(string conn = null)
        {
            MongoClient client = new MongoClient(conn);
            _database = client.GetDatabase(DbSet.DB_NAME);
        }

        public async Task<Session> GetSession(ObjectId sessionId)
        {
            IMongoCollection<Session> collection = _database.GetCollection<Session>(DbSet.SESSIONS_COLLECTION);

            FilterDefinitionBuilder<Session> filterBuilder = Builders<Session>.Filter;
            FilterDefinition<Session> filter = filterBuilder.Eq(s => s.Id, sessionId);

            IAsyncCursor<Session> cursor = await collection.FindAsync(filter);
            Session session = await cursor.FirstOrDefaultAsync();

            return session;
        }

        public async Task ChangeToken(ObjectId userId, string token)
        {
            IMongoCollection<Session> collection = _database.GetCollection<Session>(DbSet.SESSIONS_COLLECTION);
            FilterDefinitionBuilder<Session> fb = Builders<Session>.Filter;
            SortDefinitionBuilder<Session> sb = Builders<Session>.Sort;
            UpdateDefinitionBuilder<Session> up = Builders<Session>.Update;

            FilterDefinition<Session> filter = fb.And(fb.Eq(s => s.UserId, userId), fb.Where(s => s.ExpirationDate < DateTime.UtcNow));
            UpdateDefinition<Session> update = up.Set(s => s.Token, token);

            await collection.FindOneAndUpdateAsync(filter, update, new FindOneAndUpdateOptions<Session>() { Sort = sb.Descending(s=>s.ExpirationDate)});
        }

        public async Task<Session> OpenSession(User user)
        {
            Session session = new Session
            {
                ExpirationDate = DateTime.UtcNow.AddDays(7),
                UserId = user.Id,
                Token = UserExt.BuildHash(user.Id.ToString() + Guid.NewGuid())
            };

            IMongoCollection<Session> collection = _database.GetCollection<Session>(DbSet.SESSIONS_COLLECTION);
            await collection.InsertOneAsync(session);

            return session;
        }

        public async Task CloseSession(ObjectId sessionId)
        {
            IMongoCollection<Session> collection = _database.GetCollection<Session>(DbSet.SESSIONS_COLLECTION);

            FilterDefinitionBuilder<Session> filterBuilder = Builders<Session>.Filter;
            FilterDefinition<Session> filter = filterBuilder.Eq(s => s.Id, sessionId);

            await collection.FindOneAndDeleteAsync(filter);
        }
    }
}
