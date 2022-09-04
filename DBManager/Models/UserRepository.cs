using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBManager.Models
{
    public class UserRepository
    {
        private DBManagerContext _dbContext { get; set; }
        public List<User> Users { get; set; }   

        public UserRepository (DBManagerContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public List<User> GetAllUsers()
        {
            return _dbContext.Users.ToList<User>();
        }

        public User GetUser(int id)
        {
            var user = _dbContext.Users.First(x => x.Id == id);

            if (user == null) return null;
            return user;
        }

        public int AddNewUser(User user)
        {
            _dbContext.Users.Add(user);
            try
            {
                _dbContext.SaveChanges();
                return _dbContext.Users.Max(x => x.Id);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }

        private Boolean UserExists(int userId)
        {
            var user = _dbContext.Users.Where(x=>x.Id == userId).FirstOrDefault();
            if (user == null) return false;
            return true;
        }
        public Boolean RemoveUser(int userId)
        {

            if(UserExists(userId)) 
            { 
                _dbContext.Remove(GetUser(userId));
                try
                {
                    _dbContext.SaveChanges();
                    return true;
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"error. {ex.Message.ToString()} ");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}
