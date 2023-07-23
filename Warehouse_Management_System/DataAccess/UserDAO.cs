using BusinessObjects;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class UserDAO
    {
        private static UserDAO _instance = null;
        private static MyDbContext _context = null;
        private UserDAO()
        {
        }
        public static UserDAO GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserDAO();
                }

                _context = new MyDbContext();
                return _instance;
            }
        }

        public List<User> GetUsers() => _context.Users.Include(r => r.Role).Include(l => l.Location).Include(u => u.Manager).ToList();
        public List<User> GetUsersByAdminId(int? id) => _context.Users.Include(r => r.Role).Include(l => l.Location).Include(u => u.Manager).Where(c => c.ReportsTo == id).ToList();
        public User GetUserById(int id) => _context.Users.Include(r => r.Role).Include(l => l.Location).Include(u => u.Manager).SingleOrDefault(c => c.Id == id);
        public User GetUserByLastId() => _context.Users.Include(r => r.Role).Include(l => l.Location).Include(u => u.Manager).OrderBy(c => c.Id).LastOrDefault();
        public User GetUserByLogin(string username, string password) => _context.Users.Include(r => r.Role).Include(u => u.Manager)
            .SingleOrDefault(c => c.UserName.Equals(username) && c.Password.Equals(password));

        public void SaveUser(User User)
        {
            try
            {
                _context.Users.Add(User);
                _context.SaveChanges();
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
                _context.Entry<User>(user).State = EntityState.Modified;
                _context.SaveChanges();
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
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
