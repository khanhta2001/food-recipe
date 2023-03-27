using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using WebApp.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        private readonly DataService _dataService;
        
        public UserController(ILogger<HomeController> logger, DataService dataService)
        {
            _logger = logger;
            _dataService = dataService;
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("LoginPage")]
        public IActionResult LoginPage()
        {
            return View("Login");
        }
        
        
        [AllowAnonymous]
        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return Redirect("Home");
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
        [Route("Register")]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var currentUser = registerViewModel.Username;

            UserViewModel existingUser = this._dataService.FindVariable<UserViewModel>(currentUser,"UserViewModel");
            if (existingUser != null)
            {
                return Redirect("RegisterPage");
            }
            
            PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
            
            UserViewModel user = new UserViewModel()
            {
                Username = registerViewModel.Username,
                Password = passwordHasher.HashPassword(null, registerViewModel.Password),
                Summary = "Please add here!"
            };
            this._dataService.AddModel<UserViewModel>(user, "UserAccounts");
            return View("Login");
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
