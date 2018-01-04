using System.Collections.Generic;
using System.Threading.Tasks;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Db.Entities.Models.Enums;
using HomeApp.Core.Extentions.Filters.Models;
using HomeApp.Core.Extentions.Sorts.Models.Enums;
using HomeApp.Core.Models;
using HomeApp.Core.Repositories.Contracts;
using HomeApp.Site.Utils;
using HomeApp.Site.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Microsoft.Extensions.Options;

namespace HomeApp.Site.Controllers
{
    [Authorize]
    [Route("RealEstate")]
    public class RealEstateController : Controller
    {

        private readonly IRealEstateRepository _realEstateRepository;
        private readonly IUserRepository _userRepository;
        private readonly AppOptions _options;

        public RealEstateController(IRealEstateRepository realEstateRepository,
            IUserRepository userRepository,
            IAdRepository adRepository,
            IOptions<AppOptions> options)
        {
            _realEstateRepository = realEstateRepository;
            _userRepository = userRepository;
            _options = options.Value;
        }

        [HttpGet]
        [Route("Grid/{operationType}")]
        public async Task<IActionResult> Grid(OperationType operationType,
            [FromQuery]string address,
            [FromQuery]UnitType? unitType,
            [FromQuery]float? costLow,
            [FromQuery]float? costHight,
            [FromQuery]float? areaLow,
            [FromQuery]float? areaHight,
            [FromQuery]string bathCount,
            [FromQuery]string bedCount,
            [FromQuery]float? livingLow,
            [FromQuery]float? livingHight,
            [FromQuery]float? yearBuiltLow,
            [FromQuery]float? yearBuiltHight,
            [FromQuery]RealEstateSort sort = RealEstateSort.FeaturedListings
            )
        {
            FlatFilter filter = new FlatFilter();
            filter.SetFilter(_options, 16, 0, operationType, new List<RealEstateStatus> { RealEstateStatus.Active },
                address, unitType, costLow, costHight, (int?)areaLow, (int?)areaHight, bathCount, bedCount, livingLow, livingHight, (int?)yearBuiltLow, (int?)yearBuiltHight);

            RealEstatesModel realEstateList = await _realEstateRepository.GetRealEstates(filter, sort);

            ViewBag.Sort = sort;
            ViewBag.CurrentPage = 1;
            ViewBag.OperationType = operationType;
            ViewBag.RealEstateBagFilter = new RealEstateBagFilterViewModel
            {
                Address = address,
                UnitType = unitType,
                BedCount = bedCount,
                BathCount = bathCount,
                CostLow = costLow,
                CostHight = costHight,
                AreaHight = areaHight,
                AreaLow = areaLow
            };
            return View(realEstateList);
        }

        [HttpGet]
        [Route("Grid/{operationType}/Page{page}")]
        public async Task<IActionResult> Grid(OperationType operationType,
            [FromQuery]string address,
            [FromQuery]UnitType? unitType,
            [FromQuery]float? costLow,
            [FromQuery]float? costHight,
            [FromQuery]float? areaLow,
            [FromQuery]float? areaHight,
            [FromQuery]string bathCount,
            [FromQuery]string bedCount,
            [FromQuery]float? livingLow,
            [FromQuery]float? livingHight,
            [FromQuery]float? yearBuiltLow,
            [FromQuery]float? yearBuiltHight,
            int page = 1,
            [FromQuery]RealEstateSort sort = RealEstateSort.FeaturedListings)
        {
            if (page <= 0) page = 1;
            int pageSize = 16;

            FlatFilter filter = new FlatFilter();
            filter.SetFilter(_options, pageSize, pageSize * page, operationType,
                new List<RealEstateStatus> { RealEstateStatus.Active },
                address, unitType, costLow, costHight, (int?)areaLow, (int?)areaHight, bathCount, bedCount, livingLow, livingHight, (int?)yearBuiltLow, (int?)yearBuiltHight);

            RealEstatesModel realEstateList = await _realEstateRepository.GetRealEstates(filter, sort);

            ViewBag.Sort = sort;
            ViewBag.CurrentPage = page;
            ViewBag.OperationType = operationType;
            ViewBag.RealEstateBagFilter = new RealEstateBagFilterViewModel
            {
                Address = address,
                UnitType = unitType,
                BedCount = bedCount,
                BathCount = bathCount,
                CostLow = costLow,
                CostHight = costHight,
                AreaHight = areaHight,
                AreaLow = areaLow
            };
            return View(realEstateList);
        }

        [HttpGet]
        [Route("List/{operationType}")]
        public async Task<IActionResult>
            List(OperationType operationType,
            [FromQuery]string address,
            [FromQuery]UnitType? unitType,
            [FromQuery]float? costLow,
            [FromQuery]float? costHight,
            [FromQuery]float? areaLow,
            [FromQuery]float? areaHight,
            [FromQuery]string bathCount,
            [FromQuery]string bedCount,
            [FromQuery]float? livingLow,
            [FromQuery]float? livingHight,
            [FromQuery]float? yearBuiltLow,
            [FromQuery]float? yearBuiltHight,
            [FromQuery]RealEstateSort sort = RealEstateSort.FeaturedListings)
        {

            FlatFilter filter = new FlatFilter();
            filter.SetFilter(_options, 16, 0, operationType, new List<RealEstateStatus> { RealEstateStatus.Active },
                address, unitType, costLow, costHight, (int?)areaLow, (int?)areaHight, bathCount, bedCount, livingLow, livingHight, (int?)yearBuiltLow, (int?)yearBuiltHight);

            RealEstatesModel realEstateList = await _realEstateRepository.GetRealEstates(filter, sort);

            ViewBag.Sort = sort;
            ViewBag.CurrentPage = 1;
            ViewBag.OperationType = operationType;
            ViewBag.RealEstateBagFilter = new RealEstateBagFilterViewModel
            {
                Address = address,
                UnitType = unitType,
                BedCount = bedCount,
                BathCount = bathCount,
                CostLow = costLow,
                CostHight = costHight,
                AreaHight = areaHight,
                AreaLow = areaLow
            };
            return View(realEstateList);
        }

        [HttpGet]
        [Route("List/{operationType}/Page{page}")]
        public async Task<IActionResult> List(OperationType operationType,
            [FromQuery]string address,
            [FromQuery]UnitType? unitType,
            [FromQuery]float? costLow,
            [FromQuery]float? costHight,
            [FromQuery]float? areaLow,
            [FromQuery]float? areaHight,
            [FromQuery]string bathCount,
            [FromQuery]string bedCount,
            [FromQuery]float? livingLow,
            [FromQuery]float? livingHight,
            [FromQuery]float? yearBuiltLow,
            [FromQuery]float? yearBuiltHight,
            int page = 1,
            [FromQuery]RealEstateSort sort = RealEstateSort.FeaturedListings)
        {
            if (page <= 0) page = 1;
            int pageSize = 16;

            FlatFilter filter = new FlatFilter();
            filter.SetFilter(_options, pageSize, pageSize * page, operationType,
                new List<RealEstateStatus> { RealEstateStatus.Active },
                address, unitType, costLow, costHight, (int?)areaLow, (int?)areaHight, bathCount, bedCount, livingLow, livingHight, (int?)yearBuiltLow, (int?)yearBuiltHight);

            RealEstatesModel realEstateList = await _realEstateRepository.GetRealEstates(filter, sort);

            ViewBag.Sort = sort;
            ViewBag.CurrentPage = page;
            ViewBag.OperationType = operationType;
            ViewBag.RealEstateBagFilter = new RealEstateBagFilterViewModel
            {
                Address = address,
                UnitType = unitType,
                BedCount = bedCount,
                BathCount = bathCount,
                CostLow = costLow,
                CostHight = costHight,
                AreaHight = areaHight,
                AreaLow = areaLow
            };
            return View(realEstateList);
        }

        [HttpGet]
        [Route("Detail/{realEstateId}")]
        public async Task<IActionResult> Detail(string realEstateId)
        {
            RealEstate result = await _realEstateRepository.GetRealEstate(new ObjectId(realEstateId));
            User user = await _userRepository.GetUser(result.UserId);

            DetailViewModel detailViewModel = new DetailViewModel();
            detailViewModel.RealEstate = result;
            detailViewModel.Agent = user;

            return View(detailViewModel);
        }
    }
}
