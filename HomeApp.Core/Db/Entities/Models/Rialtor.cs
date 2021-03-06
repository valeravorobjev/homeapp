using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Core.Db.Entities.Models
{
    /// <summary>
    /// Риэлтор
    /// </summary>
    public class Realtor : PersonProfessional
    {
        /// <summary>
        /// Название специальности
        /// </summary>
        [BsonElement("job")]
        [BsonIgnoreIfNull]
        public Element Job { get; set; }
    }
}
