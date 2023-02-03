using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers;
    
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult HomePage()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
    public IActionResult RegisterPage()
    {
        return View();
    }
    
    public IActionResult Register(string username, string email, string password, string retypePassword)
    {
        return LoginPage();
    }

    public IActionResult LoginPage()
    {
        return View();
    }
    
    public IActionResult Login(string username, string email, string password, string retypePassword)
    {
        return HomePage();
    }
    
    public IActionResult VerificationPage(string username, string email, string password, string retypePassword)
    {
        return HomePage();
    }
    
    public IActionResult ResetPasswordPage()
    {
        return HomePage();
    }
    
    public IActionResult ResetPassword()
    {
        return HomePage();
    }
    
    public IActionResult Verification(string username, string email, string password, string retypePassword)
    {
        return HomePage();
    }

    public IActionResult Categories()
    {
        return View();
    }
    
    public IActionResult Categories_recipePage()
    {
        return View();
    }
    
    public IActionResult NotificationPage()
    {
        return View();
    }
    
    public IActionResult Notification()
    {
        return View();
    }
    
    public IActionResult save_recipe()
    {
        return View();
    }
    
    public IActionResult ReviewPage()
    {
        return View();
    }

    public IActionResult UserAccount()
    {
        return View();
    }

    public IActionResult review_recipePage()
    {
        return View();
    }

    public IActionResult ReviewEditPage()
    {
        return View();
    }
    
    public IActionResult ReviewEdit()
    {
        return View();
    }

    public IActionResult SearchPage()
    {
        return View();
    }
    
    public IActionResult Search()
    {
        return View();
    }
    
    public IActionResult UpdateProfilePage()
    {
        return View();
    }
    
    public IActionResult UpdateProfile()
    {
        return View();
    }
    
    public IActionResult CreateRecipePage()
    {
        return HomePage();
    }
    
    public IActionResult CreateRecipe()
    {
        return HomePage();
    }
    
    public IActionResult EditRecipePage()
    {
        return HomePage();
    }
    
    public IActionResult EditRecipe()
    {
        return HomePage();
    }
    
    public IActionResult ViewRecipe()
    {
        return HomePage();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}