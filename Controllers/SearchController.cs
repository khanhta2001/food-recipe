using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Controllers
{
    public class SearchController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("SearchResults")]
        public IActionResult SearchResults()
        {
            return View("SearchResults");
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("Search")]
        public IActionResult Search()
        {
            return View("SearchResults");
        }
    }
}