using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FoodRecipe.Services;
using FoodRecipe.Models;


namespace FoodRecipe.Controllers
{
    public class RecipeController : Controller
    {
        private readonly DataService _dataService;


        public RecipeController(DataService dataService)
        {
            _dataService = dataService;
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("CreateRecipe")]
        public IActionResult CreateRecipe()
        {
            return View("CreateRecipe");
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("CreateRecipe")]
        public IActionResult CreateRecipe(RecipeViewModel recipeViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var title = recipeViewModel.Title;
            var content = recipeViewModel.Content;
            var category = recipeViewModel.Category;
            var dietaryRestriction = recipeViewModel.DietaryRestriction;

            var recipe = new RecipeViewModel()
            {
                Title = title,
                Content = content,
                Category = category,
                DietaryRestriction = dietaryRestriction
            };
            
            this._dataService.AddModel<RecipeViewModel>(recipe, "RecipeViewModel");
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
        [HttpPost]
        [Route("EditRecipe")]
        public IActionResult EditRecipe(RecipeViewModel recipeViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var title = recipeViewModel.Title;
            var content = recipeViewModel.Content;
            var category = recipeViewModel.Category;
            var dietaryRestriction = recipeViewModel.DietaryRestriction;

            this._dataService.ChangeModel<RecipeViewModel>(recipeViewModel.Id,"RecipeViewModel", "Id", "Title", title);
            this._dataService.ChangeModel<RecipeViewModel>(recipeViewModel.Id,"RecipeViewModel", "Id", "content", content);
            this._dataService.ChangeModel<RecipeViewModel>(recipeViewModel.Id,"RecipeViewModel", "Id", "category", category);
            this._dataService.ChangeModel<RecipeViewModel>(recipeViewModel.Id,"RecipeViewModel", "Id", "dietaryRestriction", dietaryRestriction);
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
        [HttpPost]
        [Route("EditReviewRecipe")]
        public IActionResult EditReviewRecipe(ReviewViewModel reviewViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var review = reviewViewModel.Review;

            this._dataService.ChangeModel<RecipeViewModel>(reviewViewModel.Id,"RecipeViewModel", "Id", "Review", review);
            return View("ViewRecipePage");
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("ReviewRecipe")]
        public IActionResult ReviewRecipe()
        {
            return View("ReviewRecipe");
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("ReviewRecipe")]
        public IActionResult ReviewRecipe(ReviewViewModel reviewViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var review = reviewViewModel.Review;
            

            var recipe = new ReviewViewModel()
            {
                Review = review
            };
            
            this._dataService.AddModel<ReviewViewModel>(recipe, "ReviewViewModel");
            return View("ReviewRecipe");
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("SaveRecipe")]
        public IActionResult SaveRecipe(int recipeId)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return this.View();
            }

            var userId = this.User.Identity.Name;
            
            return View("ViewRecipePage");
        }
        
        [AllowAnonymous]
        [HttpPost]
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