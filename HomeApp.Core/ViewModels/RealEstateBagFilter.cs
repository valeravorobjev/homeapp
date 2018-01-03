﻿using HomeApp.Core.Db.Entities.Models.Enums;

namespace HomeApp.Core.ViewModels
{
    public class RealEstateBagFilter
    {
        public string Address { get; set; }
        public UnitType? UnitType { get; set; }
        public float? CostLow { get; set; }
        public float? CostHight { get; set; }
        public float? AreaLow { get; set; }
        public float? AreaHight { get; set; }
        public string BathCount { get; set; } 
        public string BedCount { get; set; }
    }
}
