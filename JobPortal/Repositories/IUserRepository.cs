using JobPortal.Models;

namespace JobPortal.Repositories
{
    public interface IUserRepository
    {
        TblAccount Add(TblAccount account);
        TblAccount Delete(TblAccount account);
        TblAccount Update (TblAccount account);
        TblAccount GetAccount (TblAccount account);
        List<TblAccount> GetAllAccount(TblAccount account);
    }
}
