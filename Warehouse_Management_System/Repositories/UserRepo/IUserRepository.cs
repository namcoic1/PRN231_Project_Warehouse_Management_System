﻿using BusinessObjects;

namespace Repositories.UserRepo
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUserById(int id);
        void SaveUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}