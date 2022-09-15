using Microsoft.AspNetCore.Mvc;

namespace warehousetrackingapi.Controllers
{
    public class ItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
