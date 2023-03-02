using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Controllers
{
    public class RecipeController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("CreateRecipePage")]
        public IActionResult CreateRecipePage()
        {
            return View("CreateRecipe");
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
        [Route("ViewRecipePage")]
        public IActionResult ViewRecipePage()
        {
            return View("ViewRecipePage");
        }
    }
}