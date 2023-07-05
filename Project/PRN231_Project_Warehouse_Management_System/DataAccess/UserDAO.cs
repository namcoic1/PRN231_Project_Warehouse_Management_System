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

        public List<User> GetUsers() => MyDB_Context.GetInstance().Users.ToList();
        public User GetUserById(int id) => MyDB_Context.GetInstance().Users.SingleOrDefault(c => c.UserID == id);

        public void SaveUser(User User)
        {
            try
            {
                MyDB_Context.GetInstance().Users.Add(User);
                MyDB_Context.GetInstance().SaveChanges();
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
                MyDB_Context.GetInstance().Entry<User>(user).State = EntityState.Modified;
                MyDB_Context.GetInstance().SaveChanges();
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
                MyDB_Context.GetInstance().Users.Remove(user);
                MyDB_Context.GetInstance().SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
