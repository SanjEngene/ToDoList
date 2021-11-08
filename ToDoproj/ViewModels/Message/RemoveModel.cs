using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoproj.ViewModels.Message
{
    public class RemoveModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
