using System.Diagnostics;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using FoodRecipe.Models;
using Microsoft.AspNetCore.Authorization;
using FoodRecipe.Services;

namespace FoodRecipe.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataService _dataService;
        
        public HomeController(DataService dataService)
        {
            _dataService = dataService;
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("")]
        public IActionResult HomePage()
        {
            HttpContext.Session.SetString("username", "Khanh");
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult NotificationsPage()
        {
            return View();
        }
        
        public IActionResult Notification()
        {
            return HomePage();
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("SearchResults")]
        public IActionResult SearchResults(string search)
        {
            search = HttpUtility.UrlDecode(search);
            this.ViewData["Search"] = search;
            var recipe = this._dataService.FindAllRecipes(search, "Recipe");
            return View(recipe);
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search(string searchText)
        {
            searchText = HttpUtility.UrlDecode(searchText);
            searchText = string.IsNullOrEmpty(searchText) ? string.Empty : searchText.Replace("/", string.Empty).Replace("\\", string.Empty);
            if (string.IsNullOrEmpty(searchText))
            {
                return this.RedirectToAction("HomePage", "Home");
            }
            else
            {
                return this.RedirectToAction("SearchResults", "Home", new {search = searchText});
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}
    
