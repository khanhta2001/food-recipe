using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Microsoft.AspNetCore.Http;

namespace WebApp.Controllers
{
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

        public IActionResult NotificationsPage()
        {
            return View();
        }
        
        public IActionResult Notification()
        {
            return HomePage();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}
    
