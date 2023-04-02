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
            var currentEmail = registerViewModel.Email;
            UserViewModel existingUser = this._dataService.FindVariable<UserViewModel>(currentUser,"UserViewModel", "User");
            UserViewModel existingEmail = this._dataService.FindVariable<UserViewModel>(currentEmail,"UserViewModel", "Email");
            if (existingUser != null || existingEmail != null)
            {
                return this.RedirectToAction("Register", "User");
            }
            
            var rand = new Random();
            var OTP = rand.Next(100000,999999);
            
            PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
            
            UserViewModel user = new UserViewModel()
            {
                Username = registerViewModel.Username,
                Email = registerViewModel.Email,
                Password = passwordHasher.HashPassword(null, registerViewModel.Password),
                Verified = OTP.ToString()
            };
            try
            {
                var message = new MailMessage();
                message.From = new MailAddress("testdevappfood@gmail.com");
                message.To.Add(currentEmail);
                message.Subject = "Email registration for Food Recipe Account";
                message.Body = "Hi,\n\nHere is your verification code:\n" + "\n\n" + OTP.ToString() + "\n\nThank you,\nFood Recipe Admin team";

                var smtpClient = new SmtpClient("smtp.gmail.com", 465);
                smtpClient.Credentials = new NetworkCredential("testdevappfood@gmail.com", "zunwbqrzjefhgohs");
                smtpClient.Send(message);   
            }
            catch (Exception ex)
            {
                return this.RedirectToAction("Register", "User");
            }
            this._dataService.AddModel<UserViewModel>(user, "UserViewModel");
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
