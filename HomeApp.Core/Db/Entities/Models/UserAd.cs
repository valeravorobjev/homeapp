using System.Collections.Generic;
using HomeApp.Core.Db.Entities.Models.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace HomeApp.Core.Db.Entities.Models
{
    /// <summary>
    /// Рекламная компания для пользователя
    /// </summary>
    public class UserAd: Ad
    {
        /// <summary>
        /// Тариф (позиция) определяет сегмент для пользователя. Чем выше позиция, тем больше у пользователя привелегий.
        /// </summary>
        [BsonElement("ad_rate")]
        public AdRate AdRate { get; set; }
        /// <summary>
        /// Элементы рекламной компании
        /// </summary>
        [BsonElement("user_ad_items")]
        [BsonIgnoreIfNull]
        public List<UserAdItem> UserAdItems { get; set; }
    }
}
