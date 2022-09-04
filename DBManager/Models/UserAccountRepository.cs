namespace DBManager.Models
{
    public class UserAccountRepository
    {
        private DBManagerContext _dbContext { get; set; }
        
        public UserAccount UserAccount { get; set; }
        public List<UserAccount> UserAccounts { get; set; }   

        public UserAccountRepository(DBManagerContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public List<UserAccount> GetAllUserAccounts()
        {
             
            return _dbContext.UserAccounts.ToList<UserAccount>();
        }

        public UserAccount GetUserAccount(int id)
        {
            var userAccount = _dbContext.UserAccounts.First(x => x.Id == id);

            if (userAccount == null) return null;
            return userAccount;
        }

        public List<UserAccount> GetUserAccounts(int userid)
        {
            var userAccounts = _dbContext.UserAccounts.Where (x => x.User.Id == userid).ToList<UserAccount>();
            if (userAccounts.Count == 0) return null;
            return userAccounts;
        }

        public int AddNewUserAccount(UserAccount userAccount)
        {
            _dbContext.UserAccounts.Add(userAccount);
            try
            {
                _dbContext.SaveChanges();
                return _dbContext.UserAccounts.Max(x => x.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + ". " + ex.InnerException.Message.ToString());
                return -1;
            }
        }


        private Boolean UserAccountExists(int userAccountId)
        {
            var userAccount = _dbContext.UserAccounts.Where(x => x.Id ==  userAccountId).FirstOrDefault();
            if (userAccount == null) return false;
            return true;
        }

        public Boolean RemoveUserAccount(int accountId)
        {
            if(UserAccountExists(accountId))
            {
                _dbContext.Remove(GetUserAccount(accountId));
                try
                {
                    _dbContext.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    return false;
                }
            }
            return false;
        }

    }
}
