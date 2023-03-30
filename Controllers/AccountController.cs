using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodRecipe.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("UpdateProfile")]
        public IActionResult UpdateProfile()
        {
            return View("UpdateProfile");
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("UserAccount")]
        public IActionResult UserAccount()
        {
            return View("UpdateProfile");
        }
    }
}