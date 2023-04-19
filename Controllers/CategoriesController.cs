using System.Diagnostics;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using FoodRecipe.Models;
using Microsoft.AspNetCore.Authorization;
using FoodRecipe.Services;

namespace FoodRecipe.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DataService _dataService;
        public CategoriesController(DataService dataService)
        {
            _dataService = dataService;
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("Categories")]
        public IActionResult Categories()
        {
            return View("Categories");
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("Categories_recipes")]
        public IActionResult Categories_recipes(string categoriesType)
        {
            var recipeCategories = this._dataService.FindAllRecipes(categoriesType, "RecipeViewModel");
            return View("Categories_recipes", recipeCategories);
        }
    }
}