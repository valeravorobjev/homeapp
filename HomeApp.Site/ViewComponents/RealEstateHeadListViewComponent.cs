using System.Collections.Generic;
using System.Threading.Tasks;
using HomeApp.Core.Db.Entities.Models.Enums;
using HomeApp.Core.Extentions.Filters.Models;
using HomeApp.Core.Extentions.Sorts.Models.Enums;
using HomeApp.Core.Repositories.Contracts;
using HomeApp.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HomeApp.Site.ViewComponents
{
    [ViewComponent]
    public class RealEstateHeadListViewComponent : ViewComponent
    {
        private readonly IRealEstateRepository _realEstateRepository;
        public RealEstateHeadListViewComponent(IRealEstateRepository realEstateRepository)
        {
            _realEstateRepository = realEstateRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            UnitBaseFilter filter = new UnitBaseFilter();
            filter.Take = 7;
            filter.Skip = 0;
            filter.RealEstateStatusList = new List<RealEstateStatus> { RealEstateStatus.Active };

            RealEstateList result = await _realEstateRepository.GetRealEstates(filter, RealEstateSort.FeaturedListings);
            return View(result.RealEstates);
        }
    }
}
