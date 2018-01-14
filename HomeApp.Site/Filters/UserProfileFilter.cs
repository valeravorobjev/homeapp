using System.Threading.Tasks;
using HomeApp.Core.Db.Entities;
using HomeApp.Core.Db.Entities.Models.Enums;
using HomeApp.Core.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HomeApp.Site.Filters
{
    /// <summary>
    /// Фильтр проверяет создал ли пользователь свой профайл. То есть содержится ли в базе информация о пользователе: тип пользователя и т.д.
    /// </summary>
    public class UserProfileFilter: ActionFilterAttribute
    {
        private readonly IUserRepository _userRepository;

        public UserProfileFilter(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            User user = _userRepository.GetUser(context.HttpContext.User.Identity.Name).Result;
            if (user.Membership.UserStatus == UserStatus.None)
            {
                context.Result = new RedirectToActionResult("SetUserType","Profile", new {});
            }

            return base.OnActionExecutionAsync(context, next);
        }
    }
}
