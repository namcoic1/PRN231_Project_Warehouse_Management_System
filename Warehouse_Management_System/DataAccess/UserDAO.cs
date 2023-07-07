using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class UserDAO
    {
        private static UserDAO _instance = null;
        private UserDAO() { }
        public static UserDAO GetInstance()
        {
            if (_instance == null)
            {
                _instance = new UserDAO();
            }
            return _instance;
        }

        public List<User> GetUsers() => MyDbContext.GetInstance().Users.ToList();
        public User GetUserById(int id) => MyDbContext.GetInstance().Users.SingleOrDefault(c => c.ID == id);

        public void SaveUser(User User)
        {
            try
            {
                MyDbContext.GetInstance().Users.Add(User);
                MyDbContext.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void UpdateUser(User user)
        {
            try
            {
                MyDbContext.GetInstance().Entry<User>(user).State = EntityState.Modified;
                MyDbContext.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteUser(User user)
        {
            try
            {
                MyDbContext.GetInstance().Users.Remove(user);
                MyDbContext.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
