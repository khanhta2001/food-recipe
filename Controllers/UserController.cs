using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using FoodRecipe.Models;
using FoodRecipe.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace FoodRecipe.Controllers
{
    public class UserController : Controller
    {
        private readonly DataService _dataService;

        private readonly SecretKey _secretKey;
        
        private readonly SignInManager<UserViewModel> _signInManager;

        public UserController( DataService dataService, SecretKey secretKey, SignInManager<UserViewModel> signInManager)
        {
            _dataService = dataService;
            _secretKey = secretKey;
            _signInManager = signInManager;
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
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var user = loginViewModel.Username;
            var password = loginViewModel.Password;
            
            var existingUser = this._dataService.FindVariable<UserViewModel>(user, "UserViewModel", "UserName");
            if (existingUser == null)
            {
                return this.View();
            }
            
            var thisUser = this._dataService.FindVariable<VerificationViewModel>(existingUser.Id.ToString(),"VerificationViewModel", "UserId");
            
            if (thisUser.Verification != "Verified")
            {
                return this.View();
            }
            
            
            PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(null, existingUser.Password, password);
            
            if (passwordVerificationResult == PasswordVerificationResult.Success)
            {
                await _signInManager.SignInAsync(existingUser, isPersistent: false);
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
            var existingUser = this._dataService.FindVariable<UserViewModel>(currentUser,"UserViewModel", "UserName");
            var existingEmail = this._dataService.FindVariable<UserViewModel>(currentEmail,"UserViewModel", "Email");
            if (existingUser != null || existingEmail != null)
            {
                return this.RedirectToAction("Register", "User");
            }
            
            var rand = new Random();
            var OTP = rand.Next(100000,999999);
            
            var passwordHasher = new PasswordHasher<string>();
            
            var user = new UserViewModel()
            {
                UserName = registerViewModel.Username,
                NormalizedUserName = registerViewModel.Username,
                Email = registerViewModel.Email,
                NormalizedEmail = registerViewModel.Email,
                Password = passwordHasher.HashPassword(null, registerViewModel.Password),
                SecurityStamp = Guid.NewGuid().ToString()
            };
            
            try
            {
                var message = new MailMessage();
                message.From = new MailAddress("testdevappfood@gmail.com");
                message.To.Add(currentEmail);
                message.Subject = "Email registration for Food Recipe Account";
                message.Body = "Hi,\n\nHere is your verification code:\n" + "\n\n" + OTP.ToString() + "\n\nThank you,\nFood Recipe Admin team";

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587 ) 
                {
                    Credentials = new NetworkCredential("testdevappfood@gmail.com", _secretKey.Password),
                    EnableSsl = true
                };
                smtpClient.Send(message); 
            }
            catch (Exception ex)
            {
                return this.RedirectToAction("Register", "User");
            }
            
            this._dataService.AddModel<UserViewModel>(user, "UserViewModel");
            
            var currentUserModel = this._dataService.FindVariable<UserViewModel>(currentUser,"UserViewModel", "UserName");
            
            var verification = new VerificationViewModel()
            {
                UserId = currentUserModel.Id.ToString(),
                Verification = "Register",
                OTP = OTP.ToString()
            };
            
            this._dataService.AddModel<VerificationViewModel>(verification, "VerificationViewModel");
            
            return this.RedirectToAction("Verification", "User");
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
        public IActionResult ResetPassword(ResetPasswordViewModel resetPasswordViewModel, int userId)
        {
            var passwordHasher = new PasswordHasher<string>();
            var password = passwordHasher.HashPassword(null, resetPasswordViewModel.NewPassword);
            this._dataService.ChangeModel<UserViewModel>("userId","UserViewModel", "User", "Password", password);
            return this.RedirectToAction("Login", "User");
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("Verification")]
        public IActionResult Verification()
        {
            return View("VerificationOtp");
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("VerificationOtp")]
        public IActionResult VerificationOtp(VerificationViewModel verificationViewModel)
        {
            var userOTP = verificationViewModel.OTP;
            if (userOTP == null)
            {
                return this.RedirectToAction("Verification", "User");
            }
            
            var correct = this._dataService.FindVariable<VerificationViewModel>(userOTP,"VerificationViewModel", "OTP");
            
            if (correct == null)
            {
                return View("VerificationOtp");
            }

            if (correct.Verification is "Register" or "Verify Email")
            {
                this._dataService.ChangeModel<VerificationViewModel>(userOTP,"VerificationViewModel", "OTP", "Verification", "Verified");
                
                return View("Login");
            }
            if (correct.Verification == "Forget Password")
            {
                return this.RedirectToAction("ResetPassword", "User");
            }
            
            return View("VerificationOtp");
        }
        
        [AllowAnonymous]
        [HttpGet]
        [Route("VerificationPage")]
        public IActionResult VerificationPage()
        {
            return View("VerificationPage");
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("VerificationPage")]
        public IActionResult VerifyEmail(string emailValue)
        {
            var existingEmail = this._dataService.FindVariable<UserViewModel>(emailValue,"UserViewModel", "Email");
            if (existingEmail == null)
            {
                return this.RedirectToAction("Register", "User");
            }
            
            var rand = new Random();
            var OTP = rand.Next(100000,999999);
            try
            {
                var message = new MailMessage();
                message.From = new MailAddress("testdevappfood@gmail.com");
                message.To.Add(emailValue);
                message.Subject = "Email registration for Food Recipe Account";
                message.Body = "Hi,\n\nHere is your verification code:\n" + "\n\n" + OTP.ToString() + "\n\nThank you,\nFood Recipe Admin team";

                var smtpClient = new SmtpClient("smtp.gmail.com", 465);
                smtpClient.Credentials = new NetworkCredential("testdevappfood@gmail.com", _secretKey.Password);
                smtpClient.Send(message); 
            }
            catch (Exception ex)
            {
                return this.RedirectToAction("Register", "User");
            }
            this._dataService.ChangeModel<UserViewModel>("existingEmail.UserId","UserViewModel", "Verification", "Verification", OTP.ToString());
            return View("VerificationOtp");
        }
        
        [HttpGet]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme).ConfigureAwait(false);

            return this.RedirectToAction("HomePage", "Home");
        }
        
        [HttpGet]
        [Route("access_denied")]
        public IActionResult AccessDenied()
        {
            return this.RedirectToAction("HomePage", "Home");
        }
    }
}
