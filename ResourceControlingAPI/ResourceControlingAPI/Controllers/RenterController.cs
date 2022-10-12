using Microsoft.AspNetCore.Mvc;

namespace ResourceControlingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RenterController : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            return View();
        }
    }
}
