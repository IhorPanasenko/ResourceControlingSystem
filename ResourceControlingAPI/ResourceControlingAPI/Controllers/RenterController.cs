using Microsoft.AspNetCore.Mvc;

namespace ResourceControlingAPI.Controllers
{
    public class RenterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
