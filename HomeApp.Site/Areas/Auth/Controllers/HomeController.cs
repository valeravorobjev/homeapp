using System.Threading.Tasks;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Db.Entities.Models.Enums;
using HomeApp.Core.Exceptions;
using HomeApp.Core.Repositories.Contracts;
using HomeApp.Site.Areas.Auth.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace HomeApp.Site.Areas.Auth.Controllers
{
    [Area("Auth")]
    [Route("[area]")]
    public class HomeController : Controller
    {
        private readonly IAuthRepository _authRepository;

        public HomeController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            ViewBag.Title = "Авторизация";
            return View();
        }

        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            ViewBag.Title = "Авторизация";

            if (ModelState.IsValid)
            {
                try
                {
                    Session session = await _authRepository.LogIn(loginModel.Email, loginModel.Password);
                    string url = $"http://localhost:63903?sessionId={session.Id}&token={session.Token}";

                    return Redirect(url);
                }
                catch (UserNotFoundException)
                {
                    ModelState.AddModelError("Email", "Пользователь с таким логином не найден");
                }
                catch (UserNotActivatedException)
                {
                    ModelState.AddModelError("IsActive", "Пользователь заблокирован!");
                }
            }

            return View(loginModel);
        }

        [Route("Registration")]
        [HttpGet]
        public IActionResult Registration()
        {
            ViewBag.Title = "Регистрация";
            return View();
        }

        [Route("Registration")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegistrationModel registrationModel)
        {
            ViewBag.Title = "Регистрация";

            if (registrationModel.Password != registrationModel.ConfirmPassword)
                ModelState.AddModelError("ConfirmPassword", "Пароли не совпадают");
            registrationModel.Language = Language.Ru; //TODO: Нужно разобраться с языками

            if (ModelState.IsValid)
            {
                try
                {
                    await _authRepository.Register(registrationModel.Email, registrationModel.Password,
                        registrationModel.ConfirmPassword, registrationModel.Language);
                    return View();
                }
                catch (EmailInvalidException)
                {
                    ModelState.AddModelError("Email", "Электронная почта указана не верно");
                }
                catch (LoginOrPasswordException)
                {
                    ModelState.AddModelError("Password", "Логин или пароль указаны не верно");
                }
                catch (PasswordLengthException)
                {
                    ModelState.AddModelError("Password", "Минимальная длинна пароля 6 символов, максимальная 100");
                }
                catch (UserAllReadyExistException)
                {
                    ModelState.AddModelError("Email", "Пользователь с такой электронной почтой уже разегистрирован");
                }
            }


            return View(registrationModel);
        }

        [Route("Confirm")]
        [HttpGet]
        public async Task<IActionResult> Confirm(string userId, string activeCode)
        {
            ViewBag.Title = "Подтверждение регистрации";
            try
            {
                Session session = await _authRepository.ConfirmUser(userId, activeCode);
                if (ModelState.IsValid)
                    return View(session);
            }
            catch (UserNotFoundException)
            {
                ModelState.AddModelError("Email", "Пользователь с таким логином не найден");
            }
            catch (ActiveCodeException)
            {
                ModelState.AddModelError("ActiveCode", "Код не верный код активации!");
            }
            catch (UpdateException)
            {
                ModelState.AddModelError("ActiveCode", "Не получается активировать пользователя");
            }

            return View();
        }

        [Route("Error")]
        public IActionResult Error()
        {
            return View();
        }
    }
}
