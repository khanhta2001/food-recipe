using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Services;
using WebApp.Models;


namespace WebApp.Controllers
{
    public class RecipeController : Controller
    {
        private readonly DataService dataService;


        // public RecipeController(DataContext dataContext)
        // {
        //     this.dataService = new DataService(dataContext);
        // }
        [AllowAnonymous]
        [HttpGet]
        [Route("CreateRecipePage")]
        public IActionResult CreateRecipePage()
        {
            return View("CreateRecipe");
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("CreateRecipe")]
        public IActionResult CreateRecipe(RecipeViewModel RecipeViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }
            
            return View("ViewRecipePage");
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("EditRecipe")]
        public IActionResult EditRecipe()
        {
            return View("EditRecipe");
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("EditReviewRecipe")]
        public IActionResult EditReviewRecipe()
        {
            return View("EditReviewRecipe");
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("ReviewRecipe")]
        public IActionResult ReviewRecipe()
        {
            return View("ReviewRecipe");
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("SaveRecipe")]
        public IActionResult SaveRecipe()
        {
            return View("SaveRecipe");
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("LikeRecipe")]
        public IActionResult LikeRecipe()
        {
            return View("ViewRecipePage");
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("ViewRecipePage")]
        public IActionResult ViewRecipePage()
        {
            return View("ViewRecipePage");
        }
    }
}