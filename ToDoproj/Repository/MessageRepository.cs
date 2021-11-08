using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoproj.Models;

namespace ToDoproj.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private ApplicationContext _db;
        public MessageRepository(ApplicationContext context)
        {
            _db = context;
        }
        public void Create(Message obj)
        {
            _db.Messages.Add(obj);
            Save();
        }

        public Message Get(int id)
        {
            Message message = _db.Messages.Include(m => m.User).FirstOrDefault(m => m.Id == id);
            return message;
        }

        public IEnumerable<Message> GetList()
        {
            return _db.Messages.Include(m => m.User).ToList();
        }

        public void Remove(int id)
        {
            Message message = _db.Messages.FirstOrDefault(m => m.Id == id);
            _db.Messages.Remove(message);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public void Update(Message obj)
        {
            _db.Messages.Update(obj);
            Save();
        }
    }
}
