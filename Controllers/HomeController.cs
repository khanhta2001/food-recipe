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

    public IActionResult LoginPage()
    {
        return View();
    }

    public IActionResult RegisterPage()
    {
        return View();
    }

    public IActionResult Categories()
    {
        return View();
    }

    public IActionResult Login(string username, string email, string password, string retypePassword)
    {
        return HomePage();
    }

    public IActionResult Register(string username, string email, string password, string retypePassword)
    {
        return LoginPage();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}