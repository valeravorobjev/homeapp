using HomeApp.Core.Db.Entities;
using HomeApp.Core.ViewModels.Enums;

namespace HomeApp.Site.ViewModels
{
    public class GridWidgetItemViewModel
    {
        public GridWidgetItemViewModel() { }

        public GridWidgetItemViewModel(RealEstate realEstate, GridWidgetItemWidth gridWidgetItemWidth)
        {
            GridWidgetItemWidth = gridWidgetItemWidth;
            RealEstate = realEstate;
        }

        public RealEstate RealEstate { get; set; }
        public GridWidgetItemWidth GridWidgetItemWidth { get; set; }
    }
}
