using System;
using System.Security.Claims;
using System.Threading.Tasks;
using HomeApp.Core.Db.Entities.Models.Enums;
using HomeApp.Core.Identity.CustomProvider;
using HomeApp.Core.Repositories.Contracts;
using HomeApp.Site.Areas.Auth.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HomeApp.Site.Areas.Auth.Controllers
{
    [Area("Auth")]
    [Route("[area]")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly SignInManager<CustomIdentityUser> _signInManager;
        private readonly IEmailSenderRepository _emailSender;

        public HomeController(UserManager<CustomIdentityUser> userManager,
            SignInManager<CustomIdentityUser> signInManager, IEmailSenderRepository emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [Route("")]
        [AllowAnonymous]
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
                return RedirectToAction("Index", "Home", new { area = "" });

            ViewBag.Title = "Авторизация";
            ViewBag.ReturnUrl = returnUrl;
            await HttpContext.SignOutAsync();
            return View();
        }

        [Route("Login")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, [FromQuery]string returnUrl = null)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home", new { area = "" });

            ViewBag.Title = "Авторизация";
            ViewBag.ReturnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, loginViewModel.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl ?? Url.Action("Index", "Home", new { area = "" }));
                }

                if (result.IsLockedOut)
                {
                    //return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError("Login", "Неправильный логин или пароль");
                    return View(loginViewModel);
                }
            }

            return View(loginViewModel);
        }

        [Route("ExternalLogin")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Home", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [Route("ExternalLoginCallback")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                ModelState.AddModelError("ExternalLoginCallback", $"Error from external provider: {remoteError}");
                return RedirectToAction("Login", "Home", new { area = "Auth" });
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: true, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }
            if (result.IsLockedOut)
            {
                return RedirectToAction("Lockout");
            }
            else
            {
                ViewBag.ReturnUrl = returnUrl;
                ViewBag.Title = "Регистрация";  
                ViewBag.LoginProvider = info.LoginProvider;

                string email = info.Principal.FindFirstValue(ClaimTypes.Email);
                string name = info.Principal.FindFirstValue(ClaimTypes.Name) ?? $"{info.Principal.FindFirstValue(ClaimTypes.Surname)} {info.Principal.FindFirstValue(ClaimTypes.GivenName)}";

                return View("ExternalLogin", new ExternalLoginViewModel { Email = email, UserName = name});
            }
        }

        [Route("ExternalLoginConfirmation")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                ExternalLoginInfo info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    throw new ApplicationException("Error loading external login information during confirmation.");
                }
                CustomIdentityUser user = new CustomIdentityUser { Name = model.Email, Email = model.Email, UserName = model.UserName};

                IdentityResult result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddClaimsAsync(user, info.Principal.Claims);
                    if (result.Succeeded)
                    {
                        result = await _userManager.AddLoginAsync(user, info);
                        if (result.Succeeded)
                        {
                            string tokenEmail = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            result = await _userManager.ConfirmEmailAsync(user, tokenEmail);
                            if (result.Succeeded)
                            {
                                await _signInManager.SignInAsync(user, isPersistent: true);
                                return RedirectToLocal(returnUrl);
                            }
                        }
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View("ExternalLogin", model);
        }

        [Route("Lockout")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [Route("Logout")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        [Route("Registration")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Registration(string returnUrl = null)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home", new { area = "" });

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
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home", new { area = "" });

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
                    string callbackUrl = Url.Action("Confirm", "Home", new { userId = user.Id, code, area = "Auth" }, protocol: HttpContext.Request.Scheme);
                    await _emailSender.SendEmailConfirmationAsync(user.Email, callbackUrl, Language.Ru);
                    return View("SendEmail");
                }
                AddErrors(result);
            }


            return View(registrationViewModel);
        }

        [Route("Confirm")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Confirm(string userId, string code)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home", new { area = "" });

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

            return View(result.Succeeded ? "Confirm" : "Error");
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
                return RedirectToAction("Index", "Home", new {area = ""});
            }
        }

        #endregion
    }
}
