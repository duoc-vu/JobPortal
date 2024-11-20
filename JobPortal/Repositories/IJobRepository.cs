using JobPortal.Models;
namespace JobPortal.Repositories
{
    public interface IJobRepository
    {
        TblJob Add(TblJob job);
        TblJob Update(TblJob job);
        TblJob Delete(TblJob job);
        TblJob GetJob(int jobID);

        TblJob GetJobI(int jobID);
        List<TblJob> FindJobE(int EmployerID);
        List<TblJob> GetAllJob();
    }
}
