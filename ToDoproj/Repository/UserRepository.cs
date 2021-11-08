using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoproj.Models;

namespace ToDoproj.Repository
{
    public class UserRepository : IUserRepository
    {
        private ApplicationContext _db;
        public UserRepository(ApplicationContext context)
        {
            _db = context;
        }
        public void Create(User obj)
        {
            _db.Users.Add(obj);
            Save();
        }

        public void Update(User obj)
        {
            _db.Users.Update(obj);
            Save();
        }

        public User Get(int id)
        {
            User user = _db.Users.Include(u => u.ToDos).Include(u => u.Messages).FirstOrDefault(u => u.Id == id);
            return user;
        }

        public IEnumerable<User> GetList()
        {
            return _db.Users.Include(u => u.ToDos).Include(u => u.Messages).ToList();
        }

        public void Remove(int id)
        {
            User user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _db.Users.Remove(user);
                Save();
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
