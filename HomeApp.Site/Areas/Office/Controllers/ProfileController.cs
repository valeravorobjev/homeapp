using System.Threading.Tasks;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Db.Entities.Models;
using HomeApp.Core.Db.Entities.Models.Enums;
using HomeApp.Core.Extentions;
using HomeApp.Core.Repositories.Contracts;
using HomeApp.Site.Areas.Office.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;

namespace HomeApp.Site.Areas.Office.Controllers
{
    [Area("Office")]
    [Route("[area]/[controller]/[action]/{userType?}")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public ProfileController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> SetUserType(UserType? userType)
        {
            User user = await _userRepository.GetUserAsync(HttpContext.User.Identity.Name);
            bool isRealtor;
            if (userType != null)
            {
                isRealtor = userType == UserType.Realtor;
            }
            else
                isRealtor = user.UserType == UserType.Realtor;


            SetUserTypeViewModel setUserTypeViewModel =
                new SetUserTypeViewModel { IsRealtor = isRealtor, UserId = user.Id.ToString() };

            return View(setUserTypeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SetUserType(SetUserTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userType = model.IsRealtor ? UserType.Realtor : UserType.Person;

            try
            {
                await _userRepository.SetUserTypeAsync(new ObjectId(model.UserId), userType);
            }
            catch
            {
                return View(model);
            }

            return RedirectToAction("SetPersonProfile", new { userType = userType });
        }

        [HttpGet]
        public async Task<IActionResult> SetPersonProfile(UserType userType)
        {
            Person user = (Person)await _userRepository.GetUserAsync(HttpContext.User.Identity.Name);

            SetPersonProfileViewModel model = new SetPersonProfileViewModel
            {
                UserId = user.Id.ToString(),
                UserType = userType,
                FirstName = user.FirstName?.Ru,
                LastName = user.LastName?.Ru,
                MidName = user.MidName?.Ru,
                DateBirth = user.DateBirth,
                Sex = user.Sex,
                Address = user.Address?.ToStr(Language.Ru),
                Phones = user.Phones
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SetPersonProfile(SetPersonProfileViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            model.Phones.RemoveAll(string.IsNullOrEmpty);

            Person person = new Person
            {
                FirstName = new Element { Ru = model.FirstName },
                LastName = new Element { Ru = model.LastName },
                MidName = new Element { Ru = model.MidName },
                DateBirth = model.DateBirth,
                Sex = model.Sex,
                Phones = model.Phones,
                Address = new Address()
            };

            AppOptions appOptions = _configuration.GetSection("AppOptions").Get<AppOptions>();

            person.Address.GoogleGeoCode(model.Address, Language.Ru, appOptions.Geocode.GoogleKey);

            await _userRepository.SetPersonAsync(new ObjectId(model.UserId), person);

            if (model.UserType == UserType.Realtor)
                return RedirectToAction("SetRealtorProfile", new { model.UserType });

            return RedirectToAction("SetSocial", new { model.UserType });
        }

        [HttpGet]
        public async Task<IActionResult> SetRealtorProfile(UserType userType)
        {
            SetRealtorProfileViewModel model = new SetRealtorProfileViewModel();
            model.UserType = userType;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SetRealtorProfile(SetRealtorProfileViewModel model)
        {
            return RedirectToAction("SetSocial", new { userType = model.UserType });
        }

        [HttpGet]
        public async Task<IActionResult> SetSocial(UserType userType)
        {
            User user = await _userRepository.GetUserAsync(HttpContext.User.Identity.Name);

            SetSocialViewModel model = new SetSocialViewModel
            {
                Facebook = user.SocialMedia?.Facebook,
                GooglePlus = user.SocialMedia?.GooglePlus,
                Instagram = user.SocialMedia?.Instagram,
                Ok = user.SocialMedia?.Ok,
                Twitter = user.SocialMedia?.Twitter,
                Vk = user.SocialMedia?.Vk,
                UserType = userType,
                UserId = user.Id.ToString()
            };
            model.UserType = userType;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SetSocial(SetSocialViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            SocialMedia socialMedia = new SocialMedia
            {
                Facebook = model.Facebook,
                GooglePlus = model.GooglePlus,
                Instagram = model.Instagram,
                Ok = model.Ok,
                Twitter = model.Twitter,
                Vk = model.Vk
            };

            await _userRepository.SetSocialMediaAsync(new ObjectId(model.UserId), socialMedia);

            return RedirectToAction("SetPhoto", new { userType = model.UserType });
        }

        [HttpGet]
        public async Task<IActionResult> SetPhoto(UserType userType)
        {
            SetPhotoViewModel model = new SetPhotoViewModel();
            model.UserType = userType;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SetPhoto(SetPhotoViewModel model)
        {
            return RedirectToAction("Success", new { userType = model.UserType });
        }

        [HttpGet]
        public async Task<IActionResult> Success(UserType userType)
        {
            return View(userType);
        }
    }
}