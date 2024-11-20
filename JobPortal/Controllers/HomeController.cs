using JobPortal.Models;
using JobPortal.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace JobPortal.Controllers
{
    public class HomeController : Controller
    {
        JobPortalDbContext db = new JobPortalDbContext();
        private readonly IJobRepository _jobRepository;
        private readonly IApplicantRepository _applicantRepository;

        /*        private readonly ILogger<HomeController> _logger;
        */
        public HomeController(IJobRepository jobRepository, IApplicantRepository applicantRepository)
        {
            _jobRepository = jobRepository;
            _applicantRepository = applicantRepository;
        }

        public IActionResult Index()
        {
            var lstJob = _jobRepository.GetAllJob();
            return View(lstJob);
        }

        public IActionResult JobDetail(int jobID)
        {
            var userId = HttpContext.Session.GetInt32("IUserId");
            var isApply = true;
            var jobCandidate = db.TblApplicants.FirstOrDefault(x => x.IUserId == userId && x.IJobId == jobID);
            if(jobCandidate != null)
            {
                isApply = false;
            }
            ViewBag.isApply = isApply;
            var jobDetail = _jobRepository.GetJobI(jobID);
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