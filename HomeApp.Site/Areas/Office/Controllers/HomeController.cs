using HomeApp.Site.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeApp.Site.Areas.Office.Controllers
{
    [Area("Office")]
    [Route("[area]")]
    [Authorize]
    [ServiceFilter(typeof(UserProfileFilter))]
    public class HomeController : Controller
    {
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}