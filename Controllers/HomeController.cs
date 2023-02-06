using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Microsoft.AspNetCore.Http;

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
        HttpContext.Session.SetString("username", "Khanh");
        //HttpContext.Session.SetString("SessionKeyName", "The Doctor");
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
    
    public IActionResult Logout()
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
    
    public IActionResult VerificationPage(string username, string email, string password, string retypePassword)
    {
        return View();
    }
    
    public IActionResult Verification(string username, string email, string password, string retypePassword)
    {
        return HomePage();
    }

    public IActionResult Categories()
    {
        return View();
    }
    
    public IActionResult Categories_recipes()
    {
        return View();
    }
    
    public IActionResult NotificationsPage()
    {
        return View();
    }
    
    public IActionResult Notification()
    {
        return HomePage();
    }
    
    public IActionResult save_recipesPage()
    {
        return View();
    }
    
    public IActionResult save_recipes()
    {
        return HomePage();
    }
    
    public IActionResult ReviewPage()
    {
        return View();
    }

    public IActionResult UserAccount()
    {
        return View();
    }

    public IActionResult ReviewEditPage()
    {
        return View();
    }
    
    public IActionResult ReviewEdit()
    {
        return HomePage();
    }

    public IActionResult SearchResults()
    {
        return View();
    }
    
    public IActionResult Search()
    {
        return HomePage();
    }
    
    public IActionResult UpdateProfilePage()
    {
        return View();
    }
    
    public IActionResult UpdateProfile()
    {
        return HomePage();
    }
    
    public IActionResult CreateRecipePage()
    {
        return View();
    }
    
    public IActionResult CreateRecipe()
    {
        return HomePage();
    }
    
    public IActionResult EditRecipePage()
    {
        return View();
    }
    
    public IActionResult EditRecipe()
    {
        return HomePage();
    }
    
    public IActionResult ViewRecipePage()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}