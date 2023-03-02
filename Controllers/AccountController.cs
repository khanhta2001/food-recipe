using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("UpdateProfile")]
        public IActionResult UpdateProfile()
        {
            return View("UpdateProfile");
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("UserAccount")]
        public IActionResult UserAccount()
        {
            return View("UpdateProfile");
        }
    }
}