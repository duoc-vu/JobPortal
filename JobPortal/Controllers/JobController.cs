using JobPortal.Models;
using JobPortal.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JobPortal.Controllers
{
    public class JobController : Controller
    {
        JobPortalDbContext db = new JobPortalDbContext();
        private readonly IJobRepository _jobRepository;
        private readonly IApplicantRepository _applicantRepository;

        public JobController(IJobRepository jobRepository, IApplicantRepository applicantRepository)
        {
            _jobRepository = jobRepository;
            _applicantRepository = applicantRepository;
        }

        public IActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public IActionResult JobApplicant(int jobID)
        {
            var userId = HttpContext.Session.GetInt32("IUserId");
            ViewBag.IJobId = jobID;  // Truyền JobID qua ViewBag
            ViewBag.IUserId = userId;  // Truyền UserID qua ViewBag

            return View();
        }

        [HttpPost]
        public IActionResult JobApplicant(TblApplicant applicant)
        {
            _applicantRepository.Add(applicant);
            return RedirectToAction("JobDetail", "Home" ,new {jobId = applicant.IJobId});
        }

        public IActionResult JobCandidate (int candidateID)
        {
            var jobCandidate = _applicantRepository.FindApplicantC(candidateID);
            return View(jobCandidate);
        }
    }
}
