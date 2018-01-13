using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeApp.Site.Areas.Office.Controllers
{
    [Area("Office")]
    [Route("[area]")]
    [Authorize]
    public class HomeController : Controller
    {
        [Route("")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
    }
}