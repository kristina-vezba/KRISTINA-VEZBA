using Microsoft.AspNetCore.Mvc;

namespace KRISTINA_VEZBA.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
