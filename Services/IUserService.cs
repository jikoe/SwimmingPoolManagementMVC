using BussinessObjects.Models;

namespace Services
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetUserById(int id);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
        bool Login(string username, string password);
    }
}