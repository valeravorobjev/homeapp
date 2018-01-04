using System;
using System.Collections.Generic;
using System.Security.Principal;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Core.Identity.CustomProvider
{
    [BsonIgnoreExtraElements]
    public class CustomIdentityUser : IIdentity
    {
        [BsonId]
        public virtual ObjectId Id { get; set; }
        [BsonElement("user_name")]
        [BsonIgnoreIfNull]
        public virtual string UserName { get; set; }
        [BsonElement("email")]
        [BsonIgnoreIfNull]
        public virtual string Email { get; set; }
        [BsonElement("normalized_email")]
        [BsonIgnoreIfNull]
        public virtual string NormalizedEmail { get; set; }
        [BsonElement("email_confirmed")]
        [BsonIgnoreIfNull]
        public virtual bool EmailConfirmed { get; set; }
        [BsonElement("password_hash")]
        [BsonIgnoreIfNull]
        public virtual string PasswordHash { get; set; }
        [BsonElement("security_stamp")]
        [BsonIgnoreIfNull]
        public virtual string SecurityStamp { get; set; }
        [BsonElement("roles")]
        [BsonIgnoreIfNull]
        public virtual List<string> Roles { get; set; }
        [BsonElement("logins")]
        [BsonIgnoreIfNull]
        public virtual List<IdentityUserLogin> Logins { get; set; }
        [BsonElement("claims")]
        [BsonIgnoreIfNull]
        public virtual List<CustomIdentityUserClaim> Claims { get; set; }
        [BsonElement("lockout_end_date_utc")]
        [BsonIgnoreIfNull]
        public virtual DateTimeOffset? LockoutEndDateUtc { get; set; }
        [BsonElement("tokens")]
        [BsonIgnoreIfNull]
        public virtual List<CustomIdentityUserToken> Tokens { get; set; }
        [BsonElement("access_faild_count")]
        [BsonIgnoreIfNull]
        public virtual int AccessFailedCount { get; set; }
        [BsonElement("lockout_enabled")]
        [BsonIgnoreIfNull]
        public virtual bool LockoutEnabled { get; set; }
        [BsonElement("normalized_user_name")]
        [BsonIgnoreIfNull]
        public string NormalizedUserName { get; internal set; }
        [BsonElement("authentication_type")]
        [BsonIgnoreIfNull]
        public string AuthenticationType { get; set; }
        [BsonElement("is_authenticated")]
        [BsonIgnoreIfNull]
        public bool IsAuthenticated { get; set; }
        [BsonElement("name")]
        [BsonIgnoreIfNull]
        public string Name { get; set; }
    }
}
