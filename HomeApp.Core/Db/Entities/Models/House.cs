using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Core.Db.Entities.Models
{
    /// <summary>
    /// Дом
    /// </summary>
    [BsonIgnoreExtraElements]
    public class House: Flat
    {
    }
}
