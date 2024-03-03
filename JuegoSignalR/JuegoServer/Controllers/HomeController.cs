using JuegoServer.HUB;
using JuegoServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace JuegoServer.Controllers
{
    public class HomeController : Controller
    {
        private IHubContext<JuegoHub> hubContext;

        public HomeController(IHubContext<JuegoHub> context)
        {
            hubContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
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
