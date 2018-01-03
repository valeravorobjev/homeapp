using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Db.Entities.Models.Enums;
using HomeApp.Core.Extentions.Filters.Models;
using HomeApp.Core.Extentions.Sorts.Models.Enums;
using HomeApp.Core.Models;
using HomeApp.Core.Repositories.Contracts;
using HomeApp.Site.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace HomeApp.Site.Controllers
{
    [Route("Specialist")]
    public class SpecialistController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IRealEstateRepository _realEstateRepository;
        private readonly IAdRepository _adRepository;

        public SpecialistController(IUserRepository userRepository, IRealEstateRepository realEstateRepository, IAdRepository adRepository)
        {
            _userRepository = userRepository;
            _realEstateRepository = realEstateRepository;
            _adRepository = adRepository;
        }

        [Route("Grid/{userType}")]
        public async Task<IActionResult> Grid(UserType userType, [FromQuery]string sort = "FeaturedListings")
        {
            UsersModel userList = null;
            if (userType == UserType.Realtor)
            {
                PersonProfessionalFilter filter = new PersonProfessionalFilter() { Take = 12, Skip = 0, UserTypes = new List<UserType> { userType } };
                PersonProfessionalSort personProfessionalSort =
                    (PersonProfessionalSort) Enum.Parse(typeof(PersonProfessionalSort), sort);

                userList = await _userRepository.GetPersonProfessionals(filter, personProfessionalSort);
            }
            else if (userType == UserType.Agency || userType == UserType.Developer)
            {
                ProfessionalFilter filter = new ProfessionalFilter() { Take = 12, Skip = 0, UserTypes = new List<UserType> { userType } };
                ProfessionalSort professionalSort =
                    (ProfessionalSort)Enum.Parse(typeof(ProfessionalSort), sort);

                userList = await _userRepository.GetProfessionals(filter, professionalSort);
            }

            ViewBag.Sort = sort;
            ViewBag.CurrentPage = 1;
            ViewBag.UserType = userType;
            return View(userList);

        }

        [Route("List/{userType}")]
        public async Task<IActionResult> List(UserType userType, [FromQuery]string sort = "FeaturedListings")
        {
            UsersModel userList = null;
            if (userType == UserType.Realtor)
            {
                PersonProfessionalFilter filter = new PersonProfessionalFilter() { Take = 12, Skip = 0, UserTypes = new List<UserType> { userType } };
                PersonProfessionalSort personProfessionalSort =
                    (PersonProfessionalSort)Enum.Parse(typeof(PersonProfessionalSort), sort);

                userList = await _userRepository.GetPersonProfessionals(filter, personProfessionalSort);
            }
            else if (userType == UserType.Agency || userType == UserType.Developer)
            {
                ProfessionalFilter filter = new ProfessionalFilter() { Take = 12, Skip = 0, UserTypes = new List<UserType> { userType } };
                ProfessionalSort professionalSort =
                    (ProfessionalSort)Enum.Parse(typeof(ProfessionalSort), sort);

                userList = await _userRepository.GetProfessionals(filter, professionalSort);
            }

            ViewBag.Sort = sort;
            ViewBag.CurrentPage = 1;
            ViewBag.UserType = userType;
            return View(userList);
        }

        [Route("List/{userType}/Page{page}")]
        public async Task<IActionResult> List(UserType userType, int page = 1, [FromQuery]string sort = "FeaturedListings")
        {
            if (page <= 0) page = 1;
            int pageSize = 16;

            UsersModel userList = null;
            if (userType == UserType.Realtor)
            {
                PersonProfessionalFilter filter = new PersonProfessionalFilter() { Take = pageSize, Skip = pageSize * page, UserTypes = new List<UserType> { userType } };
                PersonProfessionalSort personProfessionalSort =
                    (PersonProfessionalSort)Enum.Parse(typeof(PersonProfessionalSort), sort);

                userList = await _userRepository.GetPersonProfessionals(filter, personProfessionalSort);
            }
            else if (userType == UserType.Agency || userType == UserType.Developer)
            {
                ProfessionalFilter filter = new ProfessionalFilter() { Take = pageSize, Skip = pageSize * page, UserTypes = new List<UserType> { userType } };
                ProfessionalSort professionalSort =
                    (ProfessionalSort)Enum.Parse(typeof(ProfessionalSort), sort);

                userList = await _userRepository.GetProfessionals(filter, professionalSort);
            }

            ViewBag.Sort = sort;
            ViewBag.CurrentPage = page;
            ViewBag.UserType = userType;
            return View(userList);
        }

        [Route("Overview/{userId}")]
        public async Task<IActionResult> Overview(string userId)
        {
            User user = await _userRepository.GetUser(new ObjectId(userId));
            Ad ad = await _adRepository.GetAdForUser(user.Id);

            int soldRentEstateCount = await _realEstateRepository.RealEstateCount(new UnitBaseFilter
            {
                UserId = new ObjectId(userId),
                RealEstateStatusList = new List<RealEstateStatus> { RealEstateStatus.Sold, RealEstateStatus.Rented }
            });

            int activeRentEstateCount = await _realEstateRepository.RealEstateCount(new UnitBaseFilter
            {
                UserId = new ObjectId(userId),
                RealEstateStatusList = new List<RealEstateStatus> { RealEstateStatus.Active }
            });

            UserOverviewViewModel overview = new UserOverviewViewModel();
            overview.User = user;
            overview.Ad = ad;
            overview.SoldRentEstateCount = soldRentEstateCount;
            overview.ActiveRentEstateCount = activeRentEstateCount;

            return View(overview);
        }

        [Route("Properties/{userId}")]
        public async Task<IActionResult> Properties(string userId)
        {
            User user = await _userRepository.GetUser(new ObjectId(userId));
            Ad ad = await _adRepository.GetAdForUser(user.Id);

            RealEstatesModel realEstateList = await _realEstateRepository.GetRealEstates(new UnitBaseFilter
            {
                UserId = new ObjectId(userId),
                Take = 12,
                Skip = 0,
                RealEstateStatusList = new List<RealEstateStatus> { RealEstateStatus.Active, RealEstateStatus.Sold, RealEstateStatus.Rented }
            }, RealEstateSort.FeaturedListings);

            UserPropertiesViewModel properties = new UserPropertiesViewModel();
            properties.User = user;
            properties.Ad = ad;
            properties.RealEstateList = realEstateList;

            return View(properties);
        }

        [Route("Reviews/{userId}")]
        public async Task<IActionResult> Reviews(string userId)
        {
            User user = await _userRepository.GetUser(new ObjectId(userId));
            Ad ad = await _adRepository.GetAdForUser(user.Id);
            CommentsModel commentList = await _userRepository.GetComments(user.Id, 0, 12);

            UserReviewsViewModel reviews = new UserReviewsViewModel();
            reviews.User = user;
            reviews.Ad = ad;
            reviews.CommentList = commentList;

            return View(reviews);
        }
    }
}
