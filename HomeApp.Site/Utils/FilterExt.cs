using System.Collections.Generic;
using HomeApp.Core.Db.Entities.Models;
using HomeApp.Core.Db.Entities.Models.Enums;
using HomeApp.Core.Extentions;
using HomeApp.Core.Extentions.Filters.Models;

namespace HomeApp.Site.Utils
{
    public static class FilterExt
    {
        public static void SetFilter(this FlatFilter filter, AppOptions options,
            int take, int skip,
            OperationType operationType,
            List<RealEstateStatus> statuses, 
            string address,
            UnitType? unitType,
            float? costLow,
            float? costHight,
            int? areaLow,
            int? areaHight,
            string bathCount,
            string bedCount,
            float? livingLow,
            float? livingHight,
            int? yearBuiltLow,
            int? yearBuultHight)
        {

            filter.Take = take;
            filter.Skip = skip;
            filter.OperationTypes = new List<OperationType> {operationType};

            if (!string.IsNullOrEmpty(address))
            {
                filter.Address = new Address();
                filter.Address.GoogleGeoCode(address, Language.Ru, options.Geocode.GoogleKey);
            }

            if (unitType != null)
            {
                filter.UnitTypes = new List<UnitType> { unitType.Value };
            }

            if (statuses?.Count > 0)
            {
                filter.RealEstateStatusList = statuses;
            }

            if (costLow != null && costHight != null)
            {
                filter.Cost = new Range<float> {Low = costLow.Value, Hight = costHight.Value};
            }

            if (areaLow != null && areaHight != null)
            {
                filter.TotalArea = new Range<int> { Low = areaLow.Value, Hight = areaHight.Value};
            }

            if (!string.IsNullOrEmpty(bathCount))
            {
                string[] arr = bathCount.Split(',');
                filter.BathroomCounts = new List<byte>();

                foreach (string a in arr)
                {
                    filter.BathroomCounts.Add(byte.Parse(a));
                }
            }

            if (!string.IsNullOrEmpty(bedCount))
            {
                string[] arr = bedCount.Split(',');
                filter.RoomCounts = new List<byte>();

                foreach (string a in arr)
                {
                    filter.RoomCounts.Add(byte.Parse(a));
                }
            }

            if (livingLow != null && livingHight != null)
            {
                filter.LivingArea = new Range<int>();
                filter.LivingArea.Low = (int)livingLow;
                filter.LivingArea.Hight = (int) livingHight;
            }

            if (yearBuiltLow != null && livingHight != null)
            {
                filter.YearBuilt = new Range<int> {Low = yearBuiltLow.Value, Hight = yearBuultHight.Value};
            }
        }
    }
}
