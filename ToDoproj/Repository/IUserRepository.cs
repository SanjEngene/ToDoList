using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoproj.Models;

namespace ToDoproj.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetList();
        User Get(int id);
        void Create(User obj);
        void Update(User obj);
        void Remove(int id);
        void Save();
    }
}
