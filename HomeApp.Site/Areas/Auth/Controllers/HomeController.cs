using System.Collections.Generic;
using System.Threading.Tasks;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Db.Entities.Models.Enums;
using HomeApp.Core.Exceptions;
using HomeApp.Core.Identity.CustomProvider;
using HomeApp.Core.Repositories.Contracts;
using HomeApp.Site.Areas.Auth.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HomeApp.Site.Areas.Auth.Controllers
{
    [Area("Auth")]
    [Route("[area]")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IAuthRepository _authRepository;
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly SignInManager<CustomIdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;

        public HomeController(IAuthRepository authRepository, UserManager<CustomIdentityUser> userManager,
            SignInManager<CustomIdentityUser> signInManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authRepository = authRepository;
            _emailSender = emailSender;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Login")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            ViewBag.Title = "Авторизация";
            ViewBag.ReturnUrl = returnUrl;
            await HttpContext.SignOutAsync();
            return View();
        }

        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl = null)
        {
            ViewBag.Title = "Авторизация";
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }

                if (result.IsLockedOut)
                {
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError("Login", "Неправильный логин или пароль");
                    return View(loginViewModel);
                }

                //try
                //{
                //    Session session = await _authRepository.LogIn(loginViewModel.Email, loginViewModel.Password);
                //    string url = $"http://localhost:63903?sessionId={session.Id}&token={session.Token}";

                //    return Redirect(url);
                //}
                //catch (UserNotFoundException)
                //{
                //    ModelState.AddModelError("Email", "Пользователь с таким логином не найден");
                //}
                //catch (UserNotActivatedException)
                //{
                //    ModelState.AddModelError("IsActive", "Пользователь заблокирован!");
                //}
            }

            return View(loginViewModel);
        }

        [Route("Logout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Route("Registration")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registration(string returnUrl = null)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            ViewBag.Title = "Регистрация";
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [Route("Registration")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegistrationViewModel registrationViewModel, string returnUrl = null)
        {
            ViewBag.Title = "Регистрация";
            ViewBag.ReturnUrl = returnUrl;

            if (registrationViewModel.Password != registrationViewModel.ConfirmPassword)
                ModelState.AddModelError("ConfirmPassword", "Пароли не совпадают");
            registrationViewModel.Language = Language.Ru; //TODO: Нужно разобраться с языками

            if (ModelState.IsValid)
            {
                CustomIdentityUser user = new CustomIdentityUser { Name = registrationViewModel.Email, Email = registrationViewModel.Email };
                IdentityResult result = await _userManager.CreateAsync(user, registrationViewModel.Password);
                if (result.Succeeded)
                {
                    string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    string callbackUrl = Url.Action("Confirm","Home", new { userId = user.Id, code = code, area = "Auth" }, protocol: HttpContext.Request.Scheme);
                    await _emailSender.SendEmailConfirmationAsync(user.Email, callbackUrl, Language.Ru);
                    return RedirectToLocal(returnUrl);
                }
                AddErrors(result);

                

                //try
                //{
                //    await _authRepository.Register(registrationModel.Email, registrationModel.Password,
                //        registrationModel.ConfirmPassword, registrationModel.Language);
                //    return View();
                //}
                //catch (EmailInvalidException)
                //{
                //    ModelState.AddModelError("Email", "Электронная почта указана не верно");
                //}
                //catch (LoginOrPasswordException)
                //{
                //    ModelState.AddModelError("Password", "Логин или пароль указаны не верно");
                //}
                //catch (PasswordLengthException)
                //{
                //    ModelState.AddModelError("Password", "Минимальная длинна пароля 6 символов, максимальная 100");
                //}
                //catch (UserAllReadyExistException)
                //{
                //    ModelState.AddModelError("Email", "Пользователь с такой электронной почтой уже разегистрирован");
                //}
            }


            return View(registrationViewModel);
        }

        [Route("Confirm")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Confirm(string userId, string code)
        {
            ViewBag.Title = "Подтверждение регистрации";

            if (userId == null || code == null)
            {
                return View("Error");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("Error");
            }
            IdentityResult result = await _userManager.ConfirmEmailAsync(user, code);

            //try
            //{
            //    Session session = await _authRepository.ConfirmUser(userId, activeCode);
            //    if (ModelState.IsValid)
            //        return View(session);
            //}
            //catch (UserNotFoundException)
            //{
            //    ModelState.AddModelError("Email", "Пользователь с таким логином не найден");
            //}
            //catch (ActiveCodeException)
            //{
            //    ModelState.AddModelError("ActiveCode", "Код не верный код активации!");
            //}
            //catch (UpdateException)
            //{
            //    ModelState.AddModelError("ActiveCode", "Не получается активировать пользователя");
            //}

            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [Route("Error")]
        public IActionResult Error()
        {
            return View();
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion
    }
}
