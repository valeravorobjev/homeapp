﻿using System.Collections.Generic;
using HomeApp.Core.Db.Entities;

namespace HomeApp.Core.ViewModels
{
    /// <summary>
    /// Модель для представления. Список объектов недвижимости
    /// </summary>
    public class RealEstateList
    {
        /// <summary>
        /// Список объектов недвижимости
        /// </summary>
        public List<RealEstate> RealEstates { get; set; }
        /// <summary>
        /// Количество объектов недвижимости в базе
        /// </summary>
        public int Count { get; set; }
    }
}
