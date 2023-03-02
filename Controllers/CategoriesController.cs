using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Controllers
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
