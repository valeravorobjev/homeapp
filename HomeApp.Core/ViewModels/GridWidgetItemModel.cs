using HomeApp.Core.Db.Entities;
using HomeApp.Core.ViewModels.Enums;

namespace HomeApp.Core.ViewModels
{
    public class GridWidgetItemModel
    {
        public GridWidgetItemModel() { }

        public GridWidgetItemModel(RealEstate realEstate, GridWidgetItemWidth gridWidgetItemWidth)
        {
            GridWidgetItemWidth = gridWidgetItemWidth;
            RealEstate = realEstate;
        }

        public RealEstate RealEstate { get; set; }
        public GridWidgetItemWidth GridWidgetItemWidth { get; set; }
    }
}
