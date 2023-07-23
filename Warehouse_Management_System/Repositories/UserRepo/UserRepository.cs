using BusinessObjects;
using DataAccess;

namespace Repositories.UserRepo
{
    public class UserRepository : IUserRepository
    {
        public void SaveUser(User user) => UserDAO.GetInstance.SaveUser(user);

        public void DeleteUser(User user) => UserDAO.GetInstance.DeleteUser(user);

        public List<User> GetUsersByAdminId(int? id) => UserDAO.GetInstance.GetUsersByAdminId(id);

        public User GetUserById(int id) => UserDAO.GetInstance.GetUserById(id);

        public User GetUserByLastId() => UserDAO.GetInstance.GetUserByLastId();

        public User GetUserByLogin(string username, string password) => UserDAO.GetInstance.GetUserByLogin(username, password);

        public List<User> GetUsers() => UserDAO.GetInstance.GetUsers();

        public void UpdateUser(User user) => UserDAO.GetInstance.UpdateUser(user);
    }
}
