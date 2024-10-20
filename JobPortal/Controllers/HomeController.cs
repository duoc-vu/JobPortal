using JobPortal.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JobPortal.Controllers
{
    public class HomeController : Controller
    {
        JobPortalDbContext db = new JobPortalDbContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var lstJob = db.TblJobs.ToList();
            return View(lstJob);
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