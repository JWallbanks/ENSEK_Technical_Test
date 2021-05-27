using Microsoft.AspNetCore.Mvc;
using Web.Services;

namespace Web.Controllers
{
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IHomeControllerService _service;

        public HomeController(IHomeControllerService service)
        {
            _service = service;
        }

        [Route("~/")]
        [Route("~/[controller]/[action]")]
        public IActionResult Upload()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("{accountId}")]
        public IActionResult Details(int accountId)
        {
            return View();
        }

    }
}
