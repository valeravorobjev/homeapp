using System.Threading.Tasks;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Db.Entities.Models.Enums;
using HomeApp.Core.Repositories.Contracts;
using HomeApp.Site.Areas.Office.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace HomeApp.Site.Areas.Office.Controllers
{
    [Area("Office")]
    [Route("[area]/[controller]/[action]/{userType?}")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserRepository _userRepository;

        public ProfileController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> SetUserType(UserType? userType)
        {
            User user = await _userRepository.GetUser(HttpContext.User.Identity.Name);
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
                await _userRepository.SetUserType(new ObjectId(model.UserId), userType);
            }
            catch
            {
                return View(model);
            }

            return RedirectToAction("SetPersonProfile", new {userType = userType});
        }

        [HttpGet]
        public async Task<IActionResult> SetPersonProfile(UserType userType)
        {
            SetPersonProfileViewModel model = new SetPersonProfileViewModel();
            model.UserType = userType;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SetPersonProfile(SetPersonProfileViewModel model)
        {
            if (model.UserType == UserType.Realtor)
                return RedirectToAction("SetRealtorProfile", new { model.UserType });

            return RedirectToAction("SetSocial", new {model.UserType});
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
            return RedirectToAction("SetSocial", new {userType = model.UserType});
        }

        [HttpGet]
        public async Task<IActionResult> SetSocial(UserType userType)
        {
            SetSocialViewModel model = new SetSocialViewModel();
            model.UserType = userType;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SetSocial(SetSocialViewModel model)
        {
            return RedirectToAction("SetPhoto", new  {userType = model.UserType});
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
            return RedirectToAction("Success", new {userType = model.UserType});
        }

        [HttpGet]
        public async Task<IActionResult> Success(UserType userType)
        {
            return View(userType);
        }
    }
}