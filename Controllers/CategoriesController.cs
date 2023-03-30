using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodRecipe.Controllers
{
    public class CategoriesController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("Categories")]
        public IActionResult Categories()
        {
            return View("Categories");
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("Categories_recipes")]
        public IActionResult Categories_recipes()
        {
            return View("Categories_recipes");
        }
    }
}