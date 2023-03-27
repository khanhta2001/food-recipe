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

        private readonly IPasswordHasher<HomeController> _passwordHasher;

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
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var user = loginViewModel.Username;
            var password = loginViewModel.Password;
            
            var existingUser = this._dataService.FindVariable<UserViewModel>(user, "UserViewModel", "User");
            if (existingUser == null)
            {
                return this.View();
            }
            
            var passwordVerificationResult = _passwordHasher.VerifyHashedPassword(null, existingUser.Password, password);
            if (passwordVerificationResult == PasswordVerificationResult.Success)
            {
                return Redirect("Home");
            }
            return this.View();
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

            UserViewModel existingUser = this._dataService.FindVariable<UserViewModel>(currentUser,"UserViewModel", "User");
            if (existingUser == null)
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
            this._dataService.AddModel<UserViewModel>(user, "UserViewModel");
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
