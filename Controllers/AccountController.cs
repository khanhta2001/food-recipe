using FoodRecipe.Models;
using FoodRecipe.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodRecipe.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataService _dataService;
        public AccountController(DataService dataService)
        {
            _dataService = dataService;
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("UpdateProfilePage")]
        public IActionResult UpdateProfilePage()
        {
            return View("UpdateProfile");
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("UpdateProfile")]
        public IActionResult UpdateProfile(AccountViewModel accountViewModel, int userId)
        {
            var name = accountViewModel.Name;
            var dateOfBirth = accountViewModel.DateOfBirth;
            var summary = accountViewModel.Summary;
            this._dataService.ChangeModel<AccountViewModel>("userId","AccountViewModel", "Id", "Name", name);
            this._dataService.ChangeModel<AccountViewModel>("userId","AccountViewModel", "Id", "DateOfBirth", dateOfBirth);
            this._dataService.ChangeModel<AccountViewModel>("userId","AccountViewModel", "Id", "Summary", summary);
            return View("UserAccount");
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("UserAccount")]
        public IActionResult UserAccount()
        {
            return View();
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("FollowAccount")]
        public IActionResult FollowAccount()
        {
            return RedirectToAction("UserAccount");
        }
        
    }
}