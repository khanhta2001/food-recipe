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

        private readonly Data _data;

        public UserController( DataService dataService, Data data)
        {
            _dataService = dataService;
            _data = data;
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
            
            var existingUser = this._dataService.FindVariable<UserViewModel>(user, "UserViewModel", "User");
            if (existingUser == null)
            {
                return this.View();
            }

            if (existingUser.Verification != "Verified")
            {
                return this.View();
            }
            
            PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(null, existingUser.Password, password);
            
            if (passwordVerificationResult == PasswordVerificationResult.Success)
            {
                return this.RedirectToAction("HomePage", "Home");
            }
            
                        
            List<Claim> claims = new List<Claim>
            {
                new Claim("UserId", existingUser.Id.ToString()),
                new Claim(ClaimTypes.Name, existingUser.Username),
            };
            
            if (existingUser.IsOwner)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Owner"));
            }

            if (existingUser.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
            }

            if (!existingUser.IsOwner && !existingUser.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
            }
            
            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await this.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                }).ConfigureAwait(false);
            
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
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            var currentUser = registerViewModel.Username;
            var currentEmail = registerViewModel.Email;
            var existingUser = this._dataService.FindVariable<UserViewModel>(currentUser,"UserViewModel", "User");
            var existingEmail = this._dataService.FindVariable<UserViewModel>(currentEmail,"UserViewModel", "Email");
            if (existingUser == null || existingEmail == null)
            {
                return this.RedirectToAction("Register", "User");
            }
            
            var rand = new Random();
            var OTP = rand.Next(100000,999999);
            
            var passwordHasher = new PasswordHasher<string>();
            
            var user = new UserViewModel()
            {
                Username = registerViewModel.Username,
                Email = registerViewModel.Email,
                Password = passwordHasher.HashPassword(null, registerViewModel.Password),
                Verification = OTP.ToString()
            };
            try
            {
                var message = new MailMessage();
                message.From = new MailAddress("testdevappfood@gmail.com");
                message.To.Add(currentEmail);
                message.Subject = "Email registration for Food Recipe Account";
                message.Body = "Hi,\n\nHere is your verification code:\n" + "\n\n" + OTP.ToString() + "\n\nThank you,\nFood Recipe Admin team";

                var smtpClient = new SmtpClient("smtp.gmail.com", 465);
                smtpClient.Credentials = new NetworkCredential("testdevappfood@gmail.com", _data.Password);
                smtpClient.Send(message); 
            }
            catch (Exception ex)
            {
                return this.RedirectToAction("Register", "User");
            }
            this._dataService.AddModel<UserViewModel>(user, "UserViewModel");
            ViewData["VerificationReason"] = "Register";
            var verification = new VerificationViewModel();
            verification.VerificationReason = "Register Your Account";
            
            List<Claim> claims = new List<Claim>
            {
                new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.IsOwner ? "Owner" : user.IsAdmin ? "Admin" : "User"),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await this.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    AllowRefresh = true,
                }).ConfigureAwait(false);
            
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
        public IActionResult ResetPassword(ResetPasswordViewModel resetPasswordViewModel, int userId)
        {
            var passwordHasher = new PasswordHasher<string>();
            var password = passwordHasher.HashPassword(null, resetPasswordViewModel.NewPassword);
            this._dataService.ChangeModel<UserViewModel>(userId,"UserViewModel", "User", "Password", password);
            return this.RedirectToAction("Login", "User");
        }
        
        [AllowAnonymous]
        [HttpPost]
        [Route("VerificationOtp")]
        public IActionResult VerificationOtp(VerificationViewModel verificationViewModel)
        {
            var userOTP = verificationViewModel.OTP;
            var correct = this._dataService.FindVariable<UserViewModel>(userOTP,"UserViewModel", "User");
            if (correct == null)
            {
                return View("VerificationOtp");
            }
            this._dataService.ChangeModel<UserViewModel>(correct.Id,"UserViewModel", "Verification", "Verification", "Verified");
            
            if (verificationViewModel.VerificationReason is "Register" or "Verify Email")
            {
                return View("Login");
            }
            else if (verificationViewModel.VerificationReason == "Forget Password")
            {
                return this.RedirectToAction("ResetPassword", "User");
            }
            else
            {
                return View("VerificationOtp");
            }
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
                smtpClient.Credentials = new NetworkCredential("testdevappfood@gmail.com", _data.Password);
                smtpClient.Send(message); 
            }
            catch (Exception ex)
            {
                return this.RedirectToAction("Register", "User");
            }
            this._dataService.ChangeModel<UserViewModel>(existingEmail.Id,"UserViewModel", "Verification", "Verification", OTP.ToString());
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
