using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
                Verified = "No"
            };
            this._dataService.AddModel<UserViewModel>(user, "UserViewModel");
            var rand = new Random();
            var OTP = rand.Next(100000,999999);;
            
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("testdevappfood@gmail.com", "ozxbpxdxfkumqodf"),
                EnableSsl = true

            };

            var mailMessage = new MailMessage()
            {
                From = new MailAddress("testdevappfood@gmail.com"),
                Subject = "Email registration for Food Recipe Account",
                Body = "Hi,\n\nHere is your verification code:\n" + "\n\n" + OTP.ToString() + "Thank you,\nFood Recipe Admin team"
            };
            mailMessage.To.Add(registerViewModel.Email);
            smtpClient.Send(mailMessage);
            
            return View("VerificationOtp", OTP = OTP);
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
