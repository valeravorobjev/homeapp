using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Core.Identity.CustomProvider
{
    [BsonIgnoreExtraElements]
    public class IdentityUserLogin
    {
        [BsonElement("login_provider")]
        [BsonIgnoreIfNull]
        public string LoginProvider { get; set; }
        [BsonElement("provider_key")]
        [BsonIgnoreIfNull]
        public string ProviderKey { get; set; }
        [BsonElement("provider_display_name")]
        [BsonIgnoreIfNull]
        public string ProviderDisplayName { get; set; }
    }
}
