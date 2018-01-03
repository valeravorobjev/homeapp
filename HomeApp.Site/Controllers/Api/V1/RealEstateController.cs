using System.Collections.Generic;
using System.Threading.Tasks;
using HomeApp.Core.Db.Entities.Models.Enums;
using HomeApp.Core.Extentions.Filters.Models;
using HomeApp.Core.Extentions.Sorts.Models.Enums;
using HomeApp.Core.Repositories.Contracts;
using HomeApp.Core.ViewModels;
using HomeApp.Site.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HomeApp.Site.Controllers.Api.V1
{
    [Route("api/v1/[controller]")]
    public class RealEstateController : Controller
    {
        private readonly IRealEstateRepository _realEstateRepository;
        private readonly AppOptions _options;

        public RealEstateController(IRealEstateRepository realEstateRepository, IAdRepository adRepository, IOptions<AppOptions> options)
        {
            _realEstateRepository = realEstateRepository;
            _options = options.Value;
        }

        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetRealEstates(OperationType operationType, 
            int take, 
            int skip,
            string address,
            UnitType? unitType,
            float? costLow,
            float? costHight,
            float? areaLow,
            float? areaHight,
            string bathCount,
            string bedCount,
            float? livingLow,
            float? livingHight,
            float? yearBuiltLow,
            float? yearBuultHight,
            RealEstateSort sort = RealEstateSort.FeaturedListings)
        {
            FlatFilter filter = new FlatFilter();
            filter.SetFilter(_options, 16, 0, operationType, new List<RealEstateStatus> { RealEstateStatus.Active },
                address, unitType, costLow, costHight, (int?)areaLow, (int?)areaHight, bathCount, bedCount, livingLow, livingHight, (int?)yearBuiltLow, (int?)yearBuultHight);

            RealEstateList realEstateList = await _realEstateRepository.GetRealEstates(filter, sort);

            return Ok(realEstateList);
        }
    }
}
