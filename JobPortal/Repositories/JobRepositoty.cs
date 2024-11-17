using JobPortal.Models;

namespace JobPortal.Repositories
{
    public class JobRepositoty : IJobRepository
    {
        private readonly JobPortalDbContext _context;

        public JobRepositoty (JobPortalDbContext context)
        {
            _context = context;
        }
        public TblJob Add(TblJob job)
        {
            _context.Add(job);
            _context.SaveChanges();
            return job;
        }

        public TblJob Delete(TblJob job)
        {
            throw new NotImplementedException();
        }

        public List<TblJob> GetAllJob()
        {
            return _context.TblJobs.ToList();
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
    }
}
