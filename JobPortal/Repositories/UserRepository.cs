using JobPortal.Models;

namespace JobPortal.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly JobPortalDbContext _context;
        public UserRepository(JobPortalDbContext context) {
            _context = context;
        }
        public TblAccount Add(TblAccount account)
        {
            _context.Add(account);
            _context.SaveChanges();
            return account;
        }

        public TblAccount Delete(TblAccount account)
        {
            throw new NotImplementedException();
        }

        public TblAccount GetAccount(TblAccount account)
        {
            return _context.TblAccounts.Find(account.IUserId);
        }

        public List<TblAccount> GetAllAccount(TblAccount account)
        {
            throw new NotImplementedException();
        }

        public TblAccount Update(TblAccount account)
        {
            _context.Update(account);
            _context.SaveChanges();
            return account;
        }
    }
}
