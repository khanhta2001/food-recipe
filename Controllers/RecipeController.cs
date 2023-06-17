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
        public IActionResult CreateRecipePage()
        {
            return View("CreateRecipe");
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("CreateRecipe")]
        public async Task<IActionResult> CreateRecipe(RecipeViewModel recipeViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var title = recipeViewModel.Title;
            var content = recipeViewModel.Content;
            var category = recipeViewModel.Category;
            var dietaryRestriction = recipeViewModel.DietaryRestriction;

            var recipeModel = new RecipeViewModel()
            {
                Title = title,
                Content = content,
                Category = category,
                DietaryRestriction = dietaryRestriction
            };

            this._dataService.AddModel<RecipeViewModel>(recipeModel, "RecipeViewModel");

            var recipeReviewViewModel = new RecipeReviewViewModel()
            {
                ReviewViewModel = new ReviewViewModel(),
                RecipeViewModel = recipeModel
            };
            return View("ViewRecipePage", recipeReviewViewModel);
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

            this._dataService.ChangeModel<RecipeViewModel>("recipeViewModel.RecipeId","RecipeViewModel", "Id", "Title", title);
            this._dataService.ChangeModel<RecipeViewModel>("recipeViewModel.RecipeId","RecipeViewModel", "Id", "content", content);
            this._dataService.ChangeModel<RecipeViewModel>("recipeViewModel.RecipeId","RecipeViewModel", "Id", "category", category);
            this._dataService.ChangeModel<RecipeViewModel>("recipeViewModel.RecipeId","RecipeViewModel", "Id", "dietaryRestriction", dietaryRestriction);
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

            this._dataService.ChangeModel<RecipeViewModel>("reviewViewModel.ReviewId","RecipeViewModel", "Id", "Review", review);
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
        public IActionResult LikeRecipe(string action)
        {
            if (!this.User.Identity.IsAuthenticated)
            {
                return View("ViewRecipePage");
            }

            var recipeId = new List<string>();
            var userLikeRecipeModel = new UserLikeRecipeModel()
            {
                RecipeId = recipeId
            };
            if (action == "Like")
            {
                this._dataService.AddModel<UserLikeRecipeModel>(userLikeRecipeModel, "UserLikeRecipeModel");
            }
            else
            {
                // Remove the model, by adding in the dataService the method function
            }
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