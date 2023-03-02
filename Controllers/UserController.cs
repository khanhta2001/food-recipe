using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("LoginPage")]
        public IActionResult LoginPage()
        {
            return View("Login");
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("RegisterPage")]
        public IActionResult RegisterPage()
        {
            return View("Register");
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("RegisterPage")]
        public IActionResult Register()
        {
            return View("Register");
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("ResetPassword")]
        public IActionResult ResetPassword()
        {
            return View("ResetPassword");
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("VerificationOtp")]
        public IActionResult VerificationOtp()
        {
            return View("VerificationOTP");
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("VerificationPage")]
        public IActionResult VerificationPage()
        {
            return View("VerificationPage");
        }
    }
}
