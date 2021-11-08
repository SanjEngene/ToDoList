using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoproj.Models;

namespace ToDoproj.Repository
{
    public interface IToDoRepository
    {
        IEnumerable<ToDo> GetList();
        ToDo Get(int id);
        void Create(ToDo obj);
        void Update(ToDo obj);
        void Remove(int id);
        void Save();
    }
}
