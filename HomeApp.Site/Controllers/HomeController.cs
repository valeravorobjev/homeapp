using System.Collections.Generic;
using System.Threading.Tasks;
using HomeApp.Core.Db.Entities.Models.Enums;
using HomeApp.Core.Extentions.Filters.Models;
using HomeApp.Core.Extentions.Sorts.Models.Enums;
using HomeApp.Core.Repositories.Contracts;
using HomeApp.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HomeApp.Site.Controllers
{
    [Route("/")]

    public class HomeController : Controller
    {
        private readonly IRealEstateRepository _realEstateRepository;
        private readonly IUserRepository _userRepository;

        public HomeController(IUserRepository userRepository, IRealEstateRepository realEstateRepository)
        {
            _userRepository = userRepository;
            _realEstateRepository = realEstateRepository;
        }

        [Route("/")]
        //[ResponseCache(Duration = 3600)]
        public async Task<IActionResult> Index()
        {
            MainPage mpm = new MainPage();

            RealEstateList rents = await _realEstateRepository.GetTopRealEstates(new List<OperationType> { OperationType.Rent, OperationType.DailyRent, OperationType.LongTermRental }, 4);
            RealEstateList sale = await _realEstateRepository.GetTopRealEstates(new List<OperationType> { OperationType.Sale }, 4);
            RealEstateList locations = await _realEstateRepository.GetRealEstates(new UnitBaseFilter { Take = 9, Skip = 0 }, RealEstateSort.FeaturedListings);
            UserWithRealEstateCountList agents = await _userRepository.GetTopUsersWithRealEstateCount(new List<UserType> { UserType.Agency, UserType.Realtor }, 9);
            long allRealEstateCount = await _realEstateRepository.RealEstateCount(new UnitBaseFilter
            {
                RealEstateStatusList = new List<RealEstateStatus> { RealEstateStatus.Active }
            });

            mpm.RealEstatesTop4Rent = rents;
            mpm.RealEstatesTop4Sale = sale;
            mpm.RealEstatesTopLocation = locations;
            mpm.AgentTops = agents;
            mpm.AllActiveRealEstateCount = allRealEstateCount;

            return View(mpm);
        }

        [Route("About")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Route("Contact")]
        public IActionResult Contact()
        {
            return View();
        }

        [Route("Error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
