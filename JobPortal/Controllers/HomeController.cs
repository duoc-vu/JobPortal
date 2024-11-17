using JobPortal.Models;
using JobPortal.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JobPortal.Controllers
{
    public class HomeController : Controller
    {
        JobPortalDbContext db = new JobPortalDbContext();
        private readonly IJobRepository _jobRepository;

/*        private readonly ILogger<HomeController> _logger;
*/
        public HomeController(IJobRepository jobRepository)
        {
            _jobRepository = jobRepository;
        }

        public IActionResult Index()
        {
            var lstJob = _jobRepository.GetAllJob();
            return View(lstJob);
        }

        public IActionResult JobDetail(int jobID)
        {
            var jobDetail = _jobRepository.GetJob(jobID);
            return View(jobDetail);
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