using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Core.Identity.CustomProvider
{
    [BsonIgnoreExtraElements]
    public class CustomIdentityUserToken
    {
        [BsonElement("login_provider")]
        [BsonIgnoreIfNull]
        public string LoginProvider { get; set; }
        [BsonElement("name")]
        [BsonIgnoreIfNull]
        public string Name { get; set; }
        [BsonElement("value")]
        [BsonIgnoreIfNull]
        public string Value { get; set; }
    }
}
