using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoproj.Models;

namespace ToDoproj.Repository
{
    public class ToDoRepository : IToDoRepository
    {
        private ApplicationContext _db;
        public ToDoRepository(ApplicationContext context)
        {
            _db = context;
        }
        public void Create(ToDo obj)
        {
            _db.ToDos.Add(obj);
            Save();
        }

        public ToDo Get(int id)
        {
            ToDo toDo = _db.ToDos.Include(t => t.User).FirstOrDefault(t => t.Id == id);
            return toDo;
        }

        public IEnumerable<ToDo> GetList()
        {
            return _db.ToDos.Include(t => t.User).ToList();
        }

        public void Remove(int id)
        {
            ToDo toDo = _db.ToDos.FirstOrDefault(t => t.Id == id);
            _db.ToDos.Remove(toDo);
            Save();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(ToDo obj)
        {
            _db.ToDos.Update(obj);
            Save();
        }
    }
}
