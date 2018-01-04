using System;
using HomeApp.Core.Db.Entities.Models.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Core.Identity.CustomProvider
{
    public class CustomIdentityRole
    {
        [BsonId]
        public ObjectId Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("normalized_name")]
        public string NormalizedName { get; set; }
    }
}
