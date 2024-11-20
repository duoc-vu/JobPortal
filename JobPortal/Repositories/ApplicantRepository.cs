using JobPortal.Models;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Repositories
{
    public class ApplicantRepository : IApplicantRepository
    {

        private readonly JobPortalDbContext _context;

        public ApplicantRepository(JobPortalDbContext context)
        {
            _context = context;
        }

        public TblApplicant Add(TblApplicant job)
        {
            _context.Add(job);
            _context.SaveChanges();
            return job;
        }

        public TblApplicant Delete(TblApplicant job)
        {
            throw new NotImplementedException();
        }

        public List<TblApplicant> FindApplicantC(int candidateID)
        {
            
            return _context.TblApplicants
                .Include(i => i.IJob)
                .Where(x => x.IUserId == candidateID).ToList();
        }

        public List<TblApplicant> GetAllApplicant()
        {
            return _context.TblApplicants .Include(i => i.IJob).Include(x => x.IUserId).ToList();
        }

        public List<TblApplicant> GetApplicantE(int employerID)
        {
            return _context.TblApplicants.Include(i => i.IJob).Include(x => x.IJob.IEmployerId == employerID).ToList();
        }

        public TblApplicant Update(TblApplicant job)
        {
            throw new NotImplementedException();
        }
    }
}
