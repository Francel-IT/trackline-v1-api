using Microsoft.AspNetCore.Mvc;

namespace warehousetrackingapi.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
