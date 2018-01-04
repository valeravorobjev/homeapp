using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Core.Identity.CustomProvider
{
    [BsonIgnoreExtraElements]
    public class IdentityUserClaim
    {
        [BsonElement("type")]
        [BsonIgnoreIfNull]
        public string Type { get; set; }
        [BsonElement("originam_issuer")]
        [BsonIgnoreIfNull]
        public string OriginalIssuer { get; set; }
        [BsonElement("issuer")]
        [BsonIgnoreIfNull]
        public string Issuer { get; set; }
        [BsonElement("value_type")]
        [BsonIgnoreIfNull]
        public string ValueType { get; set; }
        [BsonElement("value")]
        [BsonIgnoreIfNull]
        public string Value { get; set; }
    }
}
