using Microsoft.AspNetCore.Mvc;
using TrackingDemoApi.Models;


namespace TrackingDemoApi.Controllers
{
    [Route("login")]
    public class LoginController : Controller
    {
        [NonAction]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("VerifyLogin")]
        public  async Task<ActionResult> VerifyLogin([FromBody] LoginModel loginModel)
        {
            if(loginModel.Email=="sa@yahoo.com" && loginModel.Password == "123")
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
    }
}
