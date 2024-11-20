using JobPortal.Models;

namespace JobPortal.Repositories
{
    public interface IApplicantRepository
    {
        TblApplicant Add(TblApplicant applicant);
        TblApplicant Update(TblApplicant applicant);
        TblApplicant Delete(TblApplicant applicant);
        List<TblApplicant> GetApplicantE(int employerID);
        List<TblApplicant> FindApplicantC(int candidateID);
        List<TblApplicant> GetAllApplicant();
    }
}
