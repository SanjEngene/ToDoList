using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoproj.Models;

namespace ToDoproj.Repository
{
    public interface IMessageRepository
    {
        IEnumerable<Message> GetList();
        Message Get(int id);
        void Create(Message obj);
        void Update(Message obj);
        void Remove(int id);
        void Save();
    }
}
