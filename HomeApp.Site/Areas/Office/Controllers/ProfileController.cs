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
    [Route("[area]/[controller]/[action]")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserRepository _userRepository;

        public ProfileController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        } 

        [HttpGet]
        public async Task<IActionResult> SetUserType()
        {
             User user = await _userRepository.GetUser(HttpContext.User.Identity.Name);
            SetUserTypeViewModel setUserTypeViewModel =
                new SetUserTypeViewModel {IsRealtor = user.UserType == UserType.Realtor, UserId = user.Id.ToString()};

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

            if (userType == UserType.Realtor)
            {
                return View(model);
            }
            else if (userType == UserType.Person)
            {
                return RedirectToAction("SetPersonProfile");
            }
            else
                return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SetPersonProfile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SetPersonProfile(SetPersonProfileViewModel model)
        {
            return RedirectToAction("SetSocial");
        }

        [HttpGet]
        public async Task<IActionResult> SetSocial()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SetSocial(SetSocialViewModel viewModel)
        {
            return RedirectToAction("SetPhoto");
        }

        [HttpGet]
        public async Task<IActionResult> SetPhoto()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SetPhoto(string photo = "")
        {
            return RedirectToAction("Success");
        }

        [HttpGet]
        public async Task<IActionResult> Success()
        {
            return View();
        }
    }
}