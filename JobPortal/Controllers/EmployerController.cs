using JobPortal.Models;
using JobPortal.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Controllers
{
    public class EmployerController : Controller
    {
        JobPortalDbContext db = new JobPortalDbContext();
        private readonly IJobRepository _jobRepository;

        public EmployerController(IJobRepository jobRepository)
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


        [Route("Employer/AddJobs")]
        [HttpGet]
        public IActionResult AddJobs()
        {
            if (db.TblEmployers != null && db.TblEmployers.Any())
            {
                ViewBag.IEmployerId = new SelectList(db.TblEmployers.ToList(), "IEmployerId", "SName");
            }
            else
            {
                ViewBag.IEmployerId = new SelectList(new List<string> { "No Employers Available" });
            }
            return View();
        }
        
        [Route("Employer/AddJobs")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddJobs(TblJob job)
        {
            if (ModelState.IsValid)
            {
                // Gán giá trị userId từ form vào iUserID của job
                var userId = HttpContext.Session.GetInt32("IUserId");
                if (userId != null)
                {
                    job.IEmployerId = userId.Value;  // Gán giá trị userId vào IEmployerId
                }

                // Thêm công việc vào cơ sở dữ liệu
                _jobRepository.Add(job);

                return RedirectToAction("Index", "Home");
            }

            return View(job);
        }

        public IActionResult JobsEmployer(int EmployerID)
        {
            var userId = HttpContext.Session.GetInt32("IUserId");
            var jobs = _jobRepository.FindJobE(userId ?? 0 );
            return View(jobs);
        }



    }
}
