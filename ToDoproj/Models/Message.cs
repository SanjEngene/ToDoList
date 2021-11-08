using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoproj.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string DateTimeStr { get; set; } = DateTime.Now.ToString("G");

        public User User { get; set; }
        public int UserId { get; set; }

    }
}
