using BusinessObjects;

namespace Repositories.UserRepo
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        List<User> GetUsersByAdminId(int? id);
        User GetUserById(int id);
        User GetUserByLastId();
        User GetUserByLogin(string username, string password);
        void SaveUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}
