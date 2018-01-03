using System;
using HomeApp.Core.Db.Entities.Models;

namespace HomeApp.Core.Extentions
{
    /// <summary>
    /// Расширения для поля Seo
    /// </summary>
    public static class SeoExt
    {
        /// <summary>
        /// Подсчет рейтинга для объекта недвижимости на основе положительных и отрицательных оценок
        /// Используется формула Вильсона
        /// </summary>
        /// <param name="seo">Объект поисковой оптимизации</param>
        public static void CalcRating(this Seo seo)
        {
            if (seo.LikeCount == 0) seo.Rating = (int)(seo.DislikeCount * -1);
            const float z = 1.64485f;
            int n = seo.LikeCount + seo.DislikeCount;

            float phat = (float)seo.LikeCount / n;

            seo.Rating = (phat + z * z / (2 * n) - z * Math.Sqrt((phat * (1 - phat) + z * z / (4 * n)) / n)) / (1 + z * z / n);
        }
    }
}
