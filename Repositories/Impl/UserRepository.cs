using BussinessObjects.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        private readonly SwimmingPoolManagementFinalContext _context = new SwimmingPoolManagementFinalContext();
        public List<User> GetAll() => _context.Users.Include(u => u.Role).ToList();

        public User GetById(int id) => _context.Users.Include(u => u.Role).FirstOrDefault(u => u.UserId == id);

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = GetById(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
        public bool FindUser(string username, string password)
        {

           var user = _context.Users.Where(c => username.Equals(c.Username) && password.Equals(c.Password)).FirstOrDefault();
            if(user != null)
            {
                return true;
            }
            return false;
        }
    }
}
