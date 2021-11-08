using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoproj.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string DayOfWeek { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
        public bool IsFinished { get; set; }

        public User User { get; set; }
        public int UserId { get; set; }
    }
}
