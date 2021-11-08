using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ToDoproj.Models
{
    public class User
    {
        public int Id { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();
        public List<ToDo> ToDos { get; set; } = new List<ToDo>();
        public string Role { get; set; }
        public string Email { get; set; }
        public string PhotoFilePath { get; set; }
        public string Password { get; set; }
        public int CountOfFinished { get; set; } = 0;
    }
}
