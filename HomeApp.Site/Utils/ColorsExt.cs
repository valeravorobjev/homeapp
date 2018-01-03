using HomeApp.Core.Db.Entities.Models.Enums;

namespace HomeApp.Site.Utils
{
    public static class ColorsExt
    {
        public static string GetRoostColor(this TermOfRent termOfRent)
        {
            if (termOfRent == TermOfRent.ChildrenAllowed)
                return "mdc-bg-red-300";

                return "mdc-bg-purple-300";
        }

        public static string GetRoostColor(this UnitProperty realEstateProperty)
        {
            if (realEstateProperty == UnitProperty.FirstTime)
                return "mdc-bg-pink-300";
            if (realEstateProperty == UnitProperty.Conditioner)
                return "mdc-bg-light-blue-500";
            if (realEstateProperty == UnitProperty.Parking)
                return "mdc-bg-amber-400";
            if (realEstateProperty == UnitProperty.Loggia)
                return "mdc-bg-teal-400";
            if (realEstateProperty == UnitProperty.Unfurnished)
                return "mdc-bg-light-green-500";
            if (realEstateProperty == UnitProperty.FreshRepair)
                return "mdc-bg-brown-400";
            if (realEstateProperty == UnitProperty.SeparateBathroom)
                return "mdc-bg-blue-grey-400";
            if (realEstateProperty == UnitProperty.StudioLayout)
                return "mdc-bg-amber-900";
            if (realEstateProperty == UnitProperty.Lift)
                return "mdc-bg-amber-A700";
            if (realEstateProperty == UnitProperty.FencedArea)
                return "mdc-bg-blue-800";
            if (realEstateProperty == UnitProperty.KitchenFurnitur)
                return "mdc-bg-blue-300";
            if (realEstateProperty == UnitProperty.WashingMachine)
                return "mdc-bg-blue-grey-700";
            if (realEstateProperty == UnitProperty.Dishwasher)
                return "mdc-bg-cyan-700";
            if (realEstateProperty == UnitProperty.Fridge)
                return "mdc-bg-deep-orange-400";
            if (realEstateProperty == UnitProperty.Phone)
                return "mdc-bg-deep-purple-500";
            if (realEstateProperty == UnitProperty.Internet)
                return "mdc-bg-green-500";
            if (realEstateProperty == UnitProperty.Tv)
                return "mdc-bg-grey-400";

            return "mdc-bg-grey-300";


        }
    }
}
