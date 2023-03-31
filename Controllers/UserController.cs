using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using FoodRecipe.Models;
using FoodRecipe.Services;

namespace FoodRecipe.Controllers
{
    public class UserController : Controller
    {
        private readonly DataService _dataService;

        public UserController( DataService dataService)
        {
            _dataService = dataService;
        }
        [AllowAnonymous]
        [HttpGet]
        [Route("Login")]
        public IActionResult Login()
        {
            return View("Login");
        }
        
        
        [AllowAnonymous]
        [HttpPost]
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
            PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(null, existingUser.Password, password);
            if (passwordVerificationResult == PasswordVerificationResult.Success)
            {
                return this.RedirectToAction("HomePage", "Home");
            }
            return this.View();
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("Register")]
        public IActionResult Register()
        {
            return View("Register");
        }

        [AllowAnonymous]
        [HttpPost]
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
            
            var rand = new Random();
            var OTP = rand.Next(100000,999999);;
            
            PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
            
            UserViewModel user = new UserViewModel()
            {
                Username = registerViewModel.Username,
                Password = passwordHasher.HashPassword(null, registerViewModel.Password),
                Verified = OTP.ToString()
            };
            this._dataService.AddModel<UserViewModel>(user, "UserViewModel");

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
            
            return View("VerificationOtp");
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("ResetPassword")]
        public IActionResult ResetPassword()
        {
            return View("ResetPassword");
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            return this.RedirectToAction("Login", "User");
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
