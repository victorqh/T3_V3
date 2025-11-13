using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization; //auth
using System.Diagnostics;
using T3_VQUISPEH.Models;

namespace T3_VQUISPEH.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        
        //auth
        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize]
        public IActionResult Arquitectura()
        {
            return View();
        }
        [Authorize]
        public IActionResult Desarrollo()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}