using JobPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Repositories
{
    public class JobRepositoty : IJobRepository
    {
        private readonly JobPortalDbContext _context;

        public JobRepositoty (JobPortalDbContext context)
        {
            _context = context;
        }
        public TblJob Add(TblJob applicant)
        {
            _context.Add(applicant);
            _context.SaveChanges();
            return applicant;
        }

        public TblJob Delete(TblJob job)
        {
            throw new NotImplementedException();
        }

        public List<TblJob> GetAllJob()
        {
            return _context.TblJobs
                .Include(j => j.IEmployer) 
                .ToList();
        }

        public TblJob GetJob(int jobID)
        {
            return _context.TblJobs.Find(jobID);
        }

        public TblJob Update(TblJob job)
        {
            _context.Update(job);
            _context.SaveChanges();
            return job;
        }

        public List<TblJob> FindJobE(int EmployerID)
        {
            var jobs = _context.TblJobs.Where(x => x.IEmployerId == EmployerID).ToList();
            return jobs;
        }

        public TblJob GetJobI(int jobID)
        {
            return _context.TblJobs
                .Include(j => j.IEmployer)
                .FirstOrDefault(i => i.IJobId == jobID); 
        }
    }
}
