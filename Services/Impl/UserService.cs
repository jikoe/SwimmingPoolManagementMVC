using BussinessObjects.Models;
using Repositories;

namespace Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetAllUsers() => _userRepository.GetAll();
        public User GetUserById(int id) => _userRepository.GetById(id);
        public void CreateUser(User user) => _userRepository.Add(user);
        public void UpdateUser(User user) => _userRepository.Update(user);
        public void DeleteUser(int id) => _userRepository.Delete(id);

        public bool Login(string username, string password)
        {
            return _userRepository.FindUser(username, password);
        }
    }
}